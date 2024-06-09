const express = require('express');
const bodyParser = require('body-parser');
const audioRoutes = require('./routes/audioRoutes');
const db = require('./config/db');

const app = express();
app.use(bodyParser.json());

app.use('/api/audio', audioRoutes);

db.sync().then(() => {
  app.listen(3003, () => {
    console.log('Audio Service is running on port 3003');
  });
}).catch(err => {
  console.error('Unable to connect to the database:', err);
});
