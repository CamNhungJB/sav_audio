const bcrypt = require('bcryptjs'); 
const jwt = require('jsonwebtoken'); 
const { User } = require('../models'); 
require('dotenv').config(); 

// Sử dụng biến môi trường để lưu trữ secret key, với giá trị mặc định nếu không có
const jwtSecret = process.env.JWT_SECRET || 'default_secret_key';


exports.register = async (req, res) => {
  const { username, password, email } = req.body; 
  const hashedPassword = await bcrypt.hash(password, 10); 
  try {
 
    const existingUser = await User.findOne({ where: { email } });
    if (existingUser) {
      return res.status(400).json({ message: 'Email already in use' });
    }

    const newUser = await User.create({ username, password: hashedPassword, email });
    res.status(201).json(newUser); 
  } catch (error) {
    res.status(400).json({ error: error.message }); 
  }
};

exports.login = async (req, res) => {
  const { username, password } = req.body; 
  try {
    const user = await User.findOne({ where: { username } }); 
    if (!user) {
      return res.status(401).json({ message: 'User not found' }); 
    }
    const validPassword = await bcrypt.compare(password, user.password); 
    if (!validPassword) {
      return res.status(401).json({ message: 'Incorrect password' }); 
    }
    // Tạo JWT với thông tin người dùng và secret key
    const token = jwt.sign(
      { id: user.id, username: user.username, role: user.role },
      jwtSecret,
      { expiresIn: '1h' } // Token hết hạn sau 1 giờ
    );
    res.json({ token }); // Trả về token
  } catch (error) {
    res.status(400).json({ error: error.message }); // Trả về lỗi nếu có
  }
};
