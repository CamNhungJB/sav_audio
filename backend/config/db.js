// db.js
const { Sequelize } = require('sequelize');

const sequelize = new Sequelize('nt219', 'username', 'password', {
  host: 'localhost',
  dialect: 'mysql',
  logging: console.log,
  pool: {
    max: 100,
    min: 0,
    acquire: 30000,
    idle: 10000
  }
});

module.exports = sequelize;
