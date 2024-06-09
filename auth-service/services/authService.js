const User = require('../models/user');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcrypt');

const secretKey = 'your_secret_key'; // Sử dụng biến môi trường cho bảo mật

async function register(data) {
  const hashedPassword = await bcrypt.hash(data.password, 10);
  return await User.create({ ...data, password: hashedPassword });
}

async function login(data) {
  const user = await User.findOne({ where: { username: data.username } });
  if (!user || !await bcrypt.compare(data.password, user.password)) {
    throw new Error('Invalid credentials');
  }
  return jwt.sign({ id: user.id, username: user.username }, secretKey);
}

module.exports = { register, login };
