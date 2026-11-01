# Cơ sở dữ liệu DA_DbQLBH

Thư mục này chứa file database SQL Server của đồ án:

- `DA_DbQLBH.mdf` – file dữ liệu
- `DA_DbQLBH_log.ldf` – file log

## Cách gắn (Attach) vào SQL Server

1. Copy 2 file trên vào thư mục dữ liệu của SQL Server (ví dụ `C:\Program Files\Microsoft SQL Server\MSSQLxx.MSSQLSERVER\MSSQL\DATA`) hoặc một thư mục SQL Server có quyền đọc/ghi.
2. Mở **SSMS** → chuột phải **Databases** → **Attach...** → **Add...** → chọn `DA_DbQLBH.mdf` → OK.
3. Database `DA_DbQLBH` xuất hiện, kèm các bảng và Stored Procedure. Chạy web như hướng dẫn trong README ở thư mục gốc.

Hoặc dùng lệnh:

```sql
CREATE DATABASE DA_DbQLBH
ON (FILENAME = 'C:\duong-dan\DA_DbQLBH.mdf'),
   (FILENAME = 'C:\duong-dan\DA_DbQLBH_log.ldf')
FOR ATTACH;
```
