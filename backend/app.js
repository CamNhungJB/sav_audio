const express = require('express');
const { BlobServiceClient, 
        BlobSASPermissions,
        StorageSharedKeyCredential, 
        generateBlobSASQueryParameters
      } = require('@azure/storage-blob');
const jwt = require('jsonwebtoken');

const bodyParser = require('body-parser');
const authRoutes = require('./routes/authRoutes');
const userRoutes = require('./routes/userRoutes');
const audioRoutes = require('./routes/audioRoutes');
const { initDb } = require('./models');

const app = express();

app.use(bodyParser.json());

app.use('/api/auth', authRoutes); // Đảm bảo sử dụng router đúng cách
app.use('/api/user', userRoutes); // Đảm bảo sử dụng router đúng cách
app.use('/api/audio', audioRoutes); // Đảm bảo sử dụng router đúng cách

const PORT = 3000;
const PRIVATE_KEY = 'qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==';
const AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=22520028blob;AccountKey=qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==;EndpointSuffix=core.windows.net";
const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);
const containerName = "22520028";
const accountName = "22520028blob";

// Middleware xác thực JWT
function authenticateToken(req, res, next) {
  const token = req.headers['authorization'];
  if (!token) return res.sendStatus(401);

  jwt.verify(token, PRIVATE_KEY, (err, user) => {
    if (err) return res.sendStatus(403);
    req.user = user;
    next();
  });
}

// Tạo SAS URL
function generateSasToken(blobName) {
  const sharedKeyCredential = new StorageSharedKeyCredential(accountName, PRIVATE_KEY);
  const containerClient = blobServiceClient.getContainerClient(containerName);
  const blobClient = containerClient.getBlobClient(blobName);

  const expiryDate = new Date();
  expiryDate.setMinutes(expiryDate.getMinutes() + 60); // URL có thời hạn 60 phút

  const sasOptions = {
    containerName: containerName,
    blobName: blobName,
    permissions: BlobSASPermissions.parse("r"), // Quyền đọc
    startsOn: new Date(),
    expiresOn: expiryDate,
  };

  const sasToken = generateBlobSASQueryParameters(sasOptions, sharedKeyCredential).toString();
  return `${blobClient.url}?${sasToken}`;
}

// Endpoint phát nhạc
app.get('/play/:blobName', authenticateToken, (req, res) => {
  const blobName = req.params.blobName;
  const sasUrl = generateSasToken(blobName);
  res.json({ url: sasUrl });
});

// initDb().then(() => {
  app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
  });
// });