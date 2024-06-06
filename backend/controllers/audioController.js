const { BlobServiceClient, StorageSharedKeyCredential, BlobSASPermissions, generateBlobSASQueryParameters } = require('@azure/storage-blob');

const AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=22520028blob;AccountKey=qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==;EndpointSuffix=core.windows.net";
const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);
const containerName = "22520028";
const accountName = "22520028blob";
const accountKey = "qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==";

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
