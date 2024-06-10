const { DataTypes } = require('sequelize');
const sequelize = require('../config/db');

const Song = sequelize.define('Song', {
  title: {type: DataTypes.STRING, allowNull: false},
  album: {type: DataTypes.STRING, allowNull: false},
  artist: {type: DataTypes.STRING, allowNull: false},
  length: {type: DataTypes.STRING, allowNull: false},
});

module.exports = Song;
