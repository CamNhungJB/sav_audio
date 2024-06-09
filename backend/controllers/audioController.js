// controllers/audioController.js
const { BlobServiceClient, StorageSharedKeyCredential, BlobSASPermissions, generateBlobSASQueryParameters } = require('@azure/storage-blob');
const sequelize = require('../config/db'); // Import the Sequelize instance
const { QueryTypes } = require('sequelize');

const AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=22520028blob;AccountKey=LNM7RAWpKPV/pgRs9nLH1S1QDDRnfATzL4l6zSkdZyg1yx0bIiiIts/mZeMfPyhsZDUr8dFI/tGp+AStBQQrag==;EndpointSuffix=core.windows.net";
const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);
const containerName = "22520028";
const accountName = "22520028blob";
const accountKey = "LNM7RAWpKPV/pgRs9nLH1S1QDDRnfATzL4l6zSkdZyg1yx0bIiiIts/mZeMfPyhsZDUr8dFI/tGp+AStBQQrag==";

exports.getSongs = async (req, res) => {
  try {
    const songs = await sequelize.query('SELECT * FROM playlist', { type: QueryTypes.SELECT });
    res.json(songs);
  } catch (error) {
    res.status(500).json({ message: 'Error fetching songs', error });
  }
};

exports.linkSongs = async (req, res) => {
  try {
    const songs = await sequelize.query('SELECT * FROM playlist', { type: QueryTypes.SELECT });
    
    const credentials = new StorageSharedKeyCredential(accountName, accountKey);
    const containerClient = blobServiceClient.getContainerClient(containerName);
    
    const sasUrls = songs.map(song => {
      const blobName = `${song.id}.m3u8`; // Assuming blob names are the song IDs
      const blobClient = containerClient.getBlobClient(blobName);
      
      const sasToken = generateBlobSASQueryParameters({
        containerName,
        blobName,
        permissions: BlobSASPermissions.parse('r'),
        expiresOn: new Date(new Date().valueOf() + 3600 * 1000) // 60 minutes expiry
      }, credentials).toString();

      const sasUrl = `${blobClient.url}?${sasToken}`;
      return { ...song, sasUrl };
    });

    res.json(sasUrls);
  } catch (error) {
    res.status(500).json({ message: 'Error generating SAS URLs', error });
  }
};

exports.streamSongs = async (req, res) => {
  const { id } = req.params; // Assuming song ID is passed as a route parameter

  try {
    const songs = await sequelize.query('SELECT * FROM playlist WHERE id = ?', {
      replacements: [id],
      type: QueryTypes.SELECT
    });
    const song = songs[0];
    
    if (!song) {
      return res.status(404).json({ message: 'Song not found' });
    }

    const blobName = `${song.id}.m3u8`;
    const containerClient = blobServiceClient.getContainerClient(containerName);
    const blobClient = containerClient.getBlobClient(blobName);

    const downloadBlockBlobResponse = await blobClient.download(0);
    
    res.setHeader('Content-Type', 'application/vnd.apple.mpegurl');
    downloadBlockBlobResponse.readableStreamBody.pipe(res);
  } catch (error) {
    res.status(500).json({ message: 'Error streaming song', error });
  }
};
