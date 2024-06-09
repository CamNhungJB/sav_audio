const { BlobServiceClient, StorageSharedKeyCredential, BlobSASPermissions, generateBlobSASQueryParameters } = require('@azure/storage-blob');
const path = require('path');
const fs = require('fs');
const hlsUtils = require('../utils/hls');
const { promisify } = require('util');
const streamPipeline = promisify(require('stream').pipeline);
const axios = require('axios');

const AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=22520028blob;AccountKey=LNM7RAWpKPV/pgRs9nLH1S1QDDRnfATzL4l6zSkdZyg1yx0bIiiIts/mZeMfPyhsZDUr8dFI/tGp+AStBQQrag==;EndpointSuffix=core.windows.net";
const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);
const containerName = "22520028";
const accountName = "22520028blob";
const accountKey = "LNM7RAWpKPV/pgRs9nLH1S1QDDRnfATzL4l6zSkdZyg1yx0bIiiIts/mZeMfPyhsZDUr8dFI/tGp+AStBQQrag==";

exports.getSongs = async (req, res) => {
  try {
    const containerClient = blobServiceClient.getContainerClient(containerName);
    let songs = [];
    for await (const blob of containerClient.listBlobsFlat()) {
      songs.push({ name: blob.name });
    }
    res.json(songs);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
};

exports.streamSong = async (req, res) => {
  const blobName = req.params.songId;
  try {
    const sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
    const containerClient = blobServiceClient.getContainerClient(containerName);
    const blobClient = containerClient.getBlobClient(blobName);

    const expiryDate = new Date();
    expiryDate.setMinutes(expiryDate.getMinutes() + 60); // URL expires in 60 minutes

    const sasOptions = {
      containerName: containerName,
      blobName: blobName,
      permissions: BlobSASPermissions.parse("r"), // Read permission
      startsOn: new Date(),
      expiresOn: expiryDate,
    };

    const sasToken = generateBlobSASQueryParameters(sasOptions, sharedKeyCredential).toString();
    const sasUrl = `${blobClient.url}?${sasToken}`;
    res.json({ url: sasUrl });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
};

exports.convertAndPlayAudio = async (req, res) => {
  const audioId = req.params.id;
  const inputFileName = `${audioId}.flac`;
  const outputDir = path.join(__dirname, '../hls', audioId);
  const inputFilePath = path.join(__dirname, '../audio_files', inputFileName);

  try {
    // Download the file from Azure Blob Storage
    const containerClient = blobServiceClient.getContainerClient(containerName);
    const blobClient = containerClient.getBlobClient(inputFileName);
    const downloadBlockBlobResponse = await blobClient.download(0);

    await streamPipeline(
      downloadBlockBlobResponse.readableStreamBody,
      fs.createWriteStream(inputFilePath)
    );

    // Convert to HLS
    await hlsUtils.convertToHLS(inputFilePath, outputDir);

    // Upload HLS files to Azure Blob Storage
    const files = fs.readdirSync(outputDir);
    for (const file of files) {
      const filePath = path.join(outputDir, file);
      const blockBlobClient = containerClient.getBlockBlobClient(`${audioId}/${file}`);
      await blockBlobClient.uploadFile(filePath);
    }

    const hlsUrl = `https://${accountName}.blob.core.windows.net/${containerName}/${audioId}/output.m3u8`;
    res.json({ url: hlsUrl });

  } catch (error) {
    res.status(500).json({ message: error.message });
  } finally {
    // Clean up local files
    fs.unlink(inputFilePath, (err) => {
      if (err) console.error(`Failed to delete input file: ${err}`);
    });
    fs.rmdir(outputDir, { recursive: true }, (err) => {
      if (err) console.error(`Failed to delete output directory: ${err}`);
    });
  }
};
