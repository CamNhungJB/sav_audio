const express = require('express');
const router = express.Router();
const audioController = require('../controllers/audioController');
const authMiddleware = require('../middleware/authMiddleware');

router.get('/songs', authMiddleware, audioController.getSongs);
router.get('/stream/:songId', authMiddleware, audioController.streamSong);

module.exports = router; // Đảm bảo export router đúng cách
