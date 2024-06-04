// Sample audioController.js
exports.getSongs = (req, res) => {
    res.json([
      { id: 1, title: 'Song 1' },
      { id: 2, title: 'Song 2' }
    ]);
  };
  
  exports.streamSong = (req, res) => {
    const songId = req.params.songId;
    const path = `./audio/${songId}.mp3`; // Đường dẫn đến file âm thanh
    res.sendFile(path, { root: '.' });
  };
  