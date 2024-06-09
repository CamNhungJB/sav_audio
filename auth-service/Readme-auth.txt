Mục đích: Quản lý xác thực người dùng và tạo JWT token.

Các File và Công dụng:

config/db.js: Thiết lập kết nối cơ sở dữ liệu bằng Sequelize.

models/user.js: Định nghĩa mô hình User với các trường như username và password.

services/authService.js: Chứa logic cho việc đăng ký người dùng và đăng nhập (băm mật khẩu và tạo JWT token).

controllers/authController.js: Xử lý các yêu cầu HTTP cho đăng ký và đăng nhập người dùng, gọi các dịch vụ tương ứng.

routes/authRoutes.js: Định nghĩa các endpoint cho đăng ký và đăng nhập người dùng.

app.js: Điểm vào chính của dịch vụ, thiết lập Express và các routes.
