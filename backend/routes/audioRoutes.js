const express = require('express');
const router = express.Router();
const audioController = require('../controllers/audioController');

router.get('/songs', audioController.getSongs);
router.get('/songs/link', audioController.linkSongs);
router.get('/songs/stream/:title', audioController.streamSongs); // Update this route to use the song title

module.exports = router;
