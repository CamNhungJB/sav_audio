const sequelize = require('../config/db');
const User = require('./user');

const initDb = async () => {
  await sequelize.sync({ force: true });
  console.log("All models were synchronized successfully.");
};

module.exports = { User, initDb };
