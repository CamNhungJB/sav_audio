const express = require('express');
const bodyParser = require('body-parser');
const authRoutes = require('./routes/authRoutes');
const db = require('./config/db');

const app = express();
const PRIVATE_KEY = 'qenp1BdYx9pnP0qCgCVbUj/KCeXF6l/btLKiSjnoyab5BdxkdFk1yB0CUNaREbCjRFSRluCiFS3k+AStpRiDJQ==';

// Middleware
app.use(bodyParser.json());

// Routes
app.use('/api/auth', authRoutes);

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
db.sync().then(() => {
  app.listen(3001, () => {
    console.log('Auth Service is running on port 3001');
  });
}).catch(err => {
  console.error('Unable to connect to the database:', err);
});
