# sav_audio
Chức năng của từng file:

config/db.js: Cấu hình kết nối cơ sở dữ liệu.
controllers/: Xử lý các yêu cầu HTTP và gọi các dịch vụ tương ứng.
middleware/: Xác thực người dùng qua JWT.
models/: Định nghĩa cấu trúc dữ liệu và tương tác với cơ sở dữ liệu.
routes/: Định nghĩa các endpoint API và liên kết với các controller.
app.js: Khởi động server và kết nối tất cả các phần lại với nhau.

1. config/db.js
Mô tả: Cấu hình kết nối cơ sở dữ liệu MySQL.
Chức năng: Chứa thông tin cần thiết để kết nối với cơ sở dữ liệu như host, user, password, và database name.
Liên quan: Được sử dụng bởi các mô hình và dịch vụ để kết nối và tương tác với cơ sở dữ liệu.

2. controllers/authController.js
Mô tả: Xử lý các yêu cầu liên quan đến xác thực (đăng nhập, đăng ký).
Chức năng: Sử dụng dịch vụ xác thực để xử lý logic liên quan đến đăng nhập và đăng ký.
Liên quan: Liên kết với authRoutes để xử lý các yêu cầu từ client.
3. controllers/userController.js
Mô tả: Xử lý các yêu cầu liên quan đến người dùng (thông tin người dùng, cập nhật thông tin).
Chức năng: Sử dụng dịch vụ người dùng để xử lý logic liên quan đến thông tin người dùng.
Liên quan: Liên kết với userRoutes để xử lý các yêu cầu từ client.
4. controllers/audioController.js
Mô tả: Xử lý các yêu cầu liên quan đến âm thanh (phát nhạc, lấy danh sách bài hát).
Chức năng: Sử dụng dịch vụ âm thanh để xử lý logic liên quan đến phát nhạc và tạo SAS URL.
Liên quan: Liên kết với audioRoutes để xử lý các yêu cầu từ client.
5. models/index.js
Mô tả: Điểm nhập để khởi tạo và quản lý các mô hình dữ liệu.
Chức năng: Kết nối với cơ sở dữ liệu và khởi tạo các mô hình.
Liên quan: Được sử dụng bởi các dịch vụ và controller để tương tác với cơ sở dữ liệu.
6. models/user.js
Mô tả: Mô hình dữ liệu người dùng.
Chức năng: Định nghĩa cấu trúc bảng và các thuộc tính của người dùng trong cơ sở dữ liệu MySQL.
Liên quan: Được sử dụng bởi userController và authController để thao tác với dữ liệu người dùng.
7. routes/authRoutes.js
Mô tả: Định nghĩa các endpoint liên quan đến xác thực (đăng nhập, đăng ký).
Chức năng: Liên kết với authController để xử lý các yêu cầu từ client.
Liên quan: Được sử dụng trong app.js để thiết lập các tuyến đường liên quan đến xác thực.
8. routes/userRoutes.js
Mô tả: Định nghĩa các endpoint liên quan đến người dùng (thông tin người dùng, cập nhật thông tin).
Chức năng: Liên kết với userController để xử lý các yêu cầu từ client.
Liên quan: Được sử dụng trong app.js để thiết lập các tuyến đường liên quan đến người dùng.
9. routes/audioRoutes.js
Mô tả: Định nghĩa các endpoint liên quan đến âm thanh (phát nhạc, lấy danh sách bài hát).
Chức năng: Liên kết với audioController để xử lý các yêu cầu từ client.
Liên quan: Được sử dụng trong app.js để thiết lập các tuyến đường liên quan đến âm thanh.
10. middleware/authMiddleware.js
Mô tả: Middleware để xác thực người dùng thông qua JWT.
Chức năng: Kiểm tra JWT trong header của yêu cầu và xác nhận tính hợp lệ.
Liên quan: Được sử dụng trong các routes để bảo vệ các endpoint yêu cầu xác thực.
11. app.js
Mô tả: File chính của ứng dụng backend.
Chức năng: Khởi động server Express.js, cấu hình các middleware và routes.


