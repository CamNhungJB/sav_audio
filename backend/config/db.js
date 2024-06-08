const { Sequelize } = require("sequelize");

const sequelize = new Sequelize("mmh", "username", "password", {
  host: "localhost",
  dialect: "mysql",
});

module.exports = sequelize;
