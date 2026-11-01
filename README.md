# WebQLBH – Web quản lý bán hàng

Đồ án môn Thiết kế Web – Đại học Nha Trang.
Ứng dụng web quản lý bán hàng xây dựng bằng **ASP.NET Core MVC**, truy cập dữ liệu qua **Entity Framework Core** và **Stored Procedure** trên **SQL Server**.

## Công nghệ

- ASP.NET Core MVC (.NET 10), C#, Razor Views, Bootstrap
- Entity Framework Core 10 + SQL Server
- Stored Procedure cho các thao tác CRUD / đăng nhập
- Xác thực dựa trên Session, phân quyền theo nhân viên

## Chức năng

- Đăng nhập / đăng xuất, đổi mật khẩu (Session, có `SessionAuthorize` filter chặn truy cập khi chưa đăng nhập)
- Quản lý danh mục: Sản phẩm, Loại sản phẩm, Nhóm sản phẩm, Đơn vị tính, Nhà cung cấp, Khách hàng
- Quản lý nhân sự: Nhân viên, Phòng ban, Quyền, Phân quyền
- Quản lý đơn mua hàng
- Danh mục địa lý: Tỉnh, Xã/Phường
- Upload ảnh sản phẩm

## Cấu trúc

```
├─ WebQLBH.slnx
├─ WebQLBH/            # Project ASP.NET Core MVC
│  ├─ Controllers/     # Controller cho từng module
│  ├─ Models/          # Entity
│  ├─ Data/            # AppDbContext (EF Core + gọi Stored Procedure)
│  ├─ Filters/         # SessionAuthorizeAttribute
│  ├─ Views/           # Razor Views
│  └─ wwwroot/         # CSS, JS, ảnh
├─ database/           # File CSDL SQL Server (.mdf, .ldf)
└─ docs/               # Slide, ảnh chụp màn hình
```

## Cách chạy

1. Cài **.NET SDK 10** và **SQL Server** (hoặc SQL Server Express/LocalDB).
2. Gắn (Attach) CSDL `DA_DbQLBH` từ 2 file `.mdf` / `.ldf` trong thư mục `database/` (xem `database/README.md`).
3. Kiểm tra chuỗi kết nối trong `WebQLBH/appsettings.json` (mặc định dùng `Server=localhost` + Windows Authentication), sửa nếu máy bạn khác.
4. Chạy:

```bash
cd WebQLBH
dotnet restore
dotnet run
```

5. Mở địa chỉ hiển thị trên terminal, đăng nhập bằng tài khoản có trong bảng nhân viên của CSDL.

## Ảnh chụp màn hình

> Thêm 2–3 ảnh vào `docs/` rồi dán vào đây: trang đăng nhập, danh sách sản phẩm, form thêm/sửa.

## Tác giả

Nhật Hoàng – Sinh viên Hệ thống thông tin quản lý, Đại học Nha Trang
