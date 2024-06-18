// controllers/audioController.js
const { BlobServiceClient, StorageSharedKeyCredential, BlobSASPermissions, generateBlobSASQueryParameters } = require('@azure/storage-blob');
const sequelize = require('../config/db'); // Import the Sequelize instance
const { QueryTypes } = require('sequelize');
require('dotenv').config(); // Đọc các biến môi trường từ file .env

// Sử dụng biến môi trường cho thông tin kết nối
const AZURE_STORAGE_CONNECTION_STRING = process.env.AZURE_STORAGE_CONNECTION_STRING;
const accountName = process.env.AZURE_ACCOUNT_NAME;
const accountKey = process.env.AZURE_ACCOUNT_KEY;
const containerName = process.env.AZURE_CONTAINER_NAME;

const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);

/**
 * Lấy danh sách các bài hát từ cơ sở dữ liệu
 */
exports.getSongs = async (req, res) => {
  try {
    const songs = await sequelize.query('SELECT * FROM playlist', { type: QueryTypes.SELECT });
    res.json(songs);
  } catch (error) {
    console.error('Error fetching songs:', error);
    res.status(500).json({ message: 'Error fetching songs', error });
  }
};

/**
 * Tạo SAS URL cho các bài hát
 */
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
    console.error('Error generating SAS URLs:', error);
    res.status(500).json({ message: 'Error generating SAS URLs', error });
  }
};

/**
 * Truyền tải bài hát
 */
exports.streamSongs = async (req, res) => {
  const { title } = req.params; // Lấy tiêu đề bài hát từ tham số request
  console.log(`Received request to stream song with title: ${title}`);

  try {
    const blobName = `${title}.m3u8`; // Điều chỉnh tên blob dựa trên cách đặt tên trong Azure
    const containerClient = blobServiceClient.getContainerClient(containerName);
    const blobClient = containerClient.getBlobClient(blobName);
    console.log('Blob client created.');

    // Tạo SAS token để truy cập an toàn
    const sasToken = generateBlobSASQueryParameters({
      containerName,
      blobName,
      permissions: BlobSASPermissions.parse('r'),
      expiresOn: new Date(new Date().valueOf() + 3600 * 1000) // 60 minutes expiry
    }, new StorageSharedKeyCredential(accountName, accountKey)).toString();

    // Tạo URL SAS hoàn chỉnh
    const sasUrl = `${blobClient.url}?${sasToken}`;
    console.log(`SAS URL generated: ${sasUrl}`);
    res.json(sasUrl); // Gửi URL SAS đến client
  } catch (error) {
    console.error('Error streaming song:', error);
    res.status(500).json({ message: 'Error streaming song', error });
  }
};
