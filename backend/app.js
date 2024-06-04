const express = require('express');
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

const PORT = process.env.PORT || 3000;

initDb().then(() => {
  app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
  });
});
