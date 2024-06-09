const sequelize = require('../config/db');
const User = require('./user');

const initDb = async () => {
  await sequelize.sync({ force: true });
  console.log("All models were synchronized successfully.");
};

module.exports = { User, initDb };


// const mysql = require('mysql2/promise');

// const initDb = async () => {
//   const connection = await mysql.createConnection({
//     host: 'localhost',
//     user: 'root',
//     password: '', // replace with your MySQL password
//     database: 'nt219'  // replace with your database name
//   });
//   return connection;
// };

// module.exports = { initDb };
