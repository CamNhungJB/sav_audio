const express = require('express');
const { BlobServiceClient, BlobSASPermissions, StorageSharedKeyCredential, generateBlobSASQueryParameters } = require('@azure/storage-blob');
const jwt = require('jsonwebtoken');
const bodyParser = require('body-parser');
const authRoutes = require('./routes/authRoutes');
const userRoutes = require('./routes/userRoutes');
const audioRoutes = require('./routes/audioRoutes');
const { initDb } = require('./models');

const app = express();

app.use(bodyParser.json());

app.use('/api/auth', authRoutes);
app.use('/api/user', userRoutes);
app.use('/api/audio', audioRoutes);

const PORT = 3000;
const PRIVATE_KEY = 'qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==';
const AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=22520028blob;AccountKey=qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==;EndpointSuffix=core.windows.net";
const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);
const containerName = "22520028";
const accountName = "22520028blob";

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
