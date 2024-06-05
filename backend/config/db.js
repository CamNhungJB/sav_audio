const { Sequelize } = require('sequelize');

const sequelize = new Sequelize('nt219', 'username', 'password', {
  host: 'localhost',
  dialect: 'mysql'
});

module.exports = sequelize;
