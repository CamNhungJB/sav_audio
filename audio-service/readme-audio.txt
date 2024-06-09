Mục đích: Quản lý và phân phối nội dung âm thanh.

Các File và Công dụng:

config/db.js: Thiết lập kết nối cơ sở dữ liệu bằng Sequelize.

models/audio.js: Định nghĩa mô hình Audio với các trường như title, artist, và url.

services/audioService.js: Chứa logic cho việc lấy thông tin audio và tạo audio mới.

controllers/audioController.js: Xử lý các yêu cầu HTTP liên quan đến âm thanh, gọi các dịch vụ tương ứng.

routes/audioRoutes.js: Định nghĩa các endpoint cho việc lấy và tạo audio.

utils/hls.js: Chứa chức năng chuyển đổi âm thanh sang định dạng HLS.

app.js: Điểm vào chính của dịch vụ, thiết lập Express và các routes.