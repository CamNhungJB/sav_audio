const express = require('express');
const bodyParser = require('body-parser');
const authRoutes = require('./routes/authRoutes');
const userRoutes = require('./routes/userRoutes');
const audioRoutes = require('./routes/audioRoutes');
const { initDb } = require('./models');

const app = express();
const PORT = 3000;
const PRIVATE_KEY = 'qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==';

// Middleware
app.use(bodyParser.json());

// Routes
app.use('/api/auth', authRoutes);
app.use('/api/user', userRoutes);
app.use('/api/audio', audioRoutes);

// JWT Authentication Middleware
const authenticateToken = (req, res, next) => {
  const token = req.headers['authorization'];
  if (!token) return res.sendStatus(401);

  jwt.verify(token.replace('Bearer ', ''), PRIVATE_KEY, (err, user) => {
    if (err) return res.sendStatus(403);
    req.user = user;
    next();
  });
};

// Start the server
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
