const express = require('express');
const router = express.Router();
const audioController = require('../controllers/audioController');  // Ensure the correct path
const authMiddleware = require('../middleware/authMiddleware');    // Ensure the correct path

router.get('/songs', authMiddleware, audioController.getSongs);
router.get('/link/:songId', authMiddleware, audioController.linkSongs);
router.get('/play/:id', audioController.streamSongs);

module.exports = router;  // Ensure export router correctly
