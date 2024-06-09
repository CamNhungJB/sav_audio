Giao tiếp giữa các microservice:

Authentication Service: Xác thực người dùng và cấp JWT.
User Service: Xác thực và quản lý thông tin người dùng, sử dụng JWT từ Authentication Service.
Audio Service: Xác thực và quản lý nội dung âm thanh, sử dụng JWT từ Authentication Service và giao tiếp với User Service để lấy thông tin người dùng.