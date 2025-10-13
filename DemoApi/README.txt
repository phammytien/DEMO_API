# 🚀 HƯỚNG DẪN CHẠY LẠI DỰ ÁN SAU KHI KHỞI ĐỘNG MÁY

## 1️⃣ Mở Docker Desktop

* Đảm bảo Docker Desktop đã mở và đang **chạy bình thường** (icon cá voi 🐳 xuất hiện, không báo lỗi).

---

## 2️⃣ Mở Terminal trong thư mục dự án

Mở Command Prompt (CMD) hoặc PowerShell tại thư mục:

```
D:\CONG_NGHE_WEB\DemoApi\

hoặc trỏ tới thư mục 

cd D:\CONG_NGHE_WEB\DemoApi\DemoApi
```

---

## 3️⃣ Khởi động lại containers

Chạy lệnh:

```
docker compose up -d
```

➡️ Lệnh này sẽ:

* Khởi động **API container (.NET)**
* Khởi động **SQL Server container**
* Tự dùng lại dữ liệu cũ (volume vẫn còn)

---

## 4️⃣ Kiểm tra trạng thái containers

Gõ:

```
docker ps
```

Kết quả ví dụ:

```
CONTAINER ID   IMAGE                                        STATUS          PORTS
abc123xyz456   demoapi:latest                               Up 10 seconds   0.0.0.0:5050->8080/tcp
def789uvw000   mcr.microsoft.com/mssql/server:2022-latest   Up 15 seconds   0.0.0.0:1435->1433/tcp
```

✅ Nếu thấy trạng thái “Up” là OK.

---

## 5️⃣ Truy cập ứng dụng

Mở trình duyệt và vào:

```
http://localhost:5050/swagger
```

---

## 6️⃣ Nếu SQL Server báo "unhealthy"

Chạy lệnh sau để xem chi tiết lỗi:

```
docker logs sqlserver
```

Sau đó có thể khởi động lại riêng container SQL bằng:

```
docker restart sqlserver
```

---

## ⚙️ Ghi chú thêm

* Nếu muốn tắt dự án:

  ```
  docker compose down
  ```
* Nếu muốn xóa hết container và rebuild lại:

  ```
  docker compose down -v
  docker compose build
  docker compose up -d
  ```

  xóa build lại từ đầu 


 docker compose down
docker compose up --build

---


Khi bạn chạy lệnh:

docker compose up --build


👉 Thì Docker Compose sẽ làm tất cả các bước sau:

Đọc file docker-compose.yml
→ biết được bạn có các service nào (ví dụ: api, sqlserver, v.v.).

Tìm và build Dockerfile của từng service có khai báo build:
→ tức là tự động build image từ Dockerfile trong thư mục đó (ví dụ DemoApi/Dockerfile).

Tạo và chạy container từ các image vừa build.
→ Nghĩa là ứng dụng .NET API + SQL Server của bạn đều được đóng gói và chạy trong container.
