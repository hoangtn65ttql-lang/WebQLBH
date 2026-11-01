using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebQLBH.Models;

namespace WebQLBH.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        // ====== NhomSP ======
        public List<NhomSP> NhomSP_DS()
        {
            return this.Database
                .SqlQueryRaw<NhomSP>("EXEC NhomSP_DS")
                .ToList();
        }

        public NhomSP? NhomSP_CT(byte maNSP)
        {
            return this.Database
                .SqlQueryRaw<NhomSP>("EXEC NhomSP_CT @MaNSP={0}", maNSP)
                .ToList()
                .FirstOrDefault();
        }

        public void NhomSP_Them(string tenNSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC NhomSP_Them @TenNSP={0}", tenNSP);
        }

        public void NhomSP_Sua(byte maNSP, string tenNSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC NhomSP_Sua @MaNSP={0}, @TenNSP={1}", maNSP, tenNSP);
        }

        public void NhomSP_Xoa(byte maNSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC NhomSP_Xoa @MaNSP={0}", maNSP);
        }

        // ====== LoaiSP ======
        public List<LoaiSP> LoaiSP_DS()
        {
            var ds = this.Database
                .SqlQueryRaw<LoaiSP>("EXEC LoaiSP_DS")
                .ToList();
            var dsNhomSP = NhomSP_DS();
            foreach (var item in ds)
                item.NhomSP = dsNhomSP.FirstOrDefault(n => n.MaNSP == item.MaNSP);
            return ds;
        }

        public LoaiSP? LoaiSP_CT(short maLSP)
        {
            var p = new[] { new SqlParameter("@MaLSP", maLSP) };
            var loaiSP = this.Database
                .SqlQueryRaw<LoaiSP>("EXEC LoaiSP_CT @MaLSP={0}", maLSP)
                .ToList()
                .FirstOrDefault();
            if (loaiSP != null)
            {
                var dsNhomSP = NhomSP_DS();
                loaiSP.NhomSP = dsNhomSP.FirstOrDefault(n => n.MaNSP == loaiSP.MaNSP);
            }
            return loaiSP;
        }

        public void LoaiSP_Them(string tenLSP, byte maNSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC LoaiSP_Them @TenLSP={0}, @MaNSP={1}", tenLSP, maNSP);
        }

        public void LoaiSP_Sua(short maLSP, string tenLSP, byte maNSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC LoaiSP_Sua @MaLSP={0}, @TenLSP={1}, @MaNSP={2}",
                maLSP, tenLSP, maNSP);
        }

        public void LoaiSP_Xoa(short maLSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC LoaiSP_Xoa @MaLSP={0}", maLSP);
        }
        public DbSet<WebQLBH.Models.LoaiSP> LoaiSP { get; set; } = default!;
        // ====== DonViTinh ======
        public List<DonViTinh> DonViTinh_DS()
        {
            return this.Database
                .SqlQueryRaw<DonViTinh>("EXEC DonViTinh_DS")
                .ToList();
        }

        public DonViTinh? DonViTinh_CT(byte maDVT)
        {
            return this.Database
                .SqlQueryRaw<DonViTinh>("EXEC DonViTinh_CT @MaDVT={0}", maDVT)
                .ToList()
                .FirstOrDefault();
        }

        public void DonViTinh_Them(string tenDVT)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonViTinh_Them @TenDVT={0}", tenDVT);
        }

        public void DonViTinh_Sua(byte maDVT, string tenDVT)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonViTinh_Sua @MaDVT={0}, @TenDVT={1}", maDVT, tenDVT);
        }

        public void DonViTinh_Xoa(byte maDVT)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonViTinh_Xoa @MaDVT={0}", maDVT);
        }
        public DbSet<WebQLBH.Models.DonViTinh> DonViTinh { get; set; } = default!;
        // ====== SanPham ======
        public List<SanPham> SanPham_DS()
        {
            var ds = SanPham.FromSqlRaw("EXEC SanPham_DS").ToList();
            var dsLoaiSP = LoaiSP_DS();
            var dsDVT = DonViTinh_DS();
            foreach (var item in ds)
            {
                item.LoaiSP = dsLoaiSP.FirstOrDefault(l => l.MaLSP == item.MaLSP);
                item.DonViTinh = dsDVT.FirstOrDefault(d => d.MaDVT == item.MaDVT);
            }
            return ds;
        }

        public SanPham? SanPham_CT(string maSP)
        {
            return this.Database
                .SqlQueryRaw<SanPham>("EXEC SanPham_CT @MaSP={0}", maSP)
                .ToList()
                .FirstOrDefault();
        }

        public void SanPham_Them(string tenSP, decimal donGia,
            string? anhSP, string? moTaCT, short maLSP, byte maDVT)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenSP", tenSP));
            cmd.Parameters.Add(new SqlParameter("@DonGia", donGia));
            cmd.Parameters.Add(new SqlParameter("@AnhSP", (object?)anhSP ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MoTaCT", (object?)moTaCT ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaLSP", maLSP));
            cmd.Parameters.Add(new SqlParameter("@MaDVT", maDVT));
            Database.OpenConnection();
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public void SanPham_Sua(string maSP, string tenSP, decimal donGia,
            string? anhSP, string? moTaCT, short maLSP, byte maDVT)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaSP", maSP));
            cmd.Parameters.Add(new SqlParameter("@TenSP", tenSP));
            cmd.Parameters.Add(new SqlParameter("@DonGia", donGia));
            cmd.Parameters.Add(new SqlParameter("@AnhSP", (object?)anhSP ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MoTaCT", (object?)moTaCT ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaLSP", maLSP));
            cmd.Parameters.Add(new SqlParameter("@MaDVT", maDVT));
            Database.OpenConnection();
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public void SanPham_Xoa(string maSP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC SanPham_Xoa @MaSP={0}", maSP);
        }
        public DbSet<WebQLBH.Models.SanPham> SanPham { get; set; } = default!;
        // ====== Tinh ======
        public List<Tinh> Tinh_DS()
        {
            return this.Database
                .SqlQueryRaw<Tinh>("EXEC Tinh_DS")
                .ToList();
        }

        public Tinh? Tinh_CT(string maTinh)
        {
            return this.Database
                .SqlQueryRaw<Tinh>("EXEC Tinh_CT @MaTinh={0}", maTinh)
                .ToList()
                .FirstOrDefault();
        }

        public void Tinh_Them(string maTinh, string tenTinh)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC Tinh_Them @MaTinh={0}, @TenTinh={1}", maTinh, tenTinh);
        }

        public void Tinh_Sua(string maTinh, string tenTinh)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC Tinh_Sua @MaTinh={0}, @TenTinh={1}", maTinh, tenTinh);
        }

        public void Tinh_Xoa(string maTinh)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC Tinh_Xoa @MaTinh={0}", maTinh);
        }
        public DbSet<WebQLBH.Models.Tinh> Tinh { get; set; } = default!;
        // ====== XaPhuong ======
        public List<XaPhuong> XaPhuong_DS()
        {
            return this.Database
                .SqlQueryRaw<XaPhuong>("EXEC XaPhuong_DS")
                .ToList();
        }

        public XaPhuong? XaPhuong_CT(string maXP)
        {
            return this.Database
                .SqlQueryRaw<XaPhuong>("EXEC XaPhuong_CT @MaXP={0}", maXP)
                .ToList()
                .FirstOrDefault();
        }

        public void XaPhuong_Them(string maXP, string tenXP, string maTinh)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC XaPhuong_Them @MaXP={0}, @TenXP={1}, @MaTinh={2}",
                maXP, tenXP, maTinh);
        }

        public void XaPhuong_Sua(string maXP, string tenXP, string maTinh)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC XaPhuong_Sua @MaXP={0}, @TenXP={1}, @MaTinh={2}",
                maXP, tenXP, maTinh);
        }

        public void XaPhuong_Xoa(string maXP)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC XaPhuong_Xoa @MaXP={0}", maXP);
        }
        public DbSet<WebQLBH.Models.XaPhuong> XaPhuong { get; set; } = default!;
        // ====== NhaCC ======
        public List<NhaCC> NhaCC_DS()
        {
            return this.Database
                .SqlQueryRaw<NhaCC>("EXEC NhaCC_DS")
                .ToList();
        }

        public NhaCC? NhaCC_CT(short maNCC)
        {
            return this.Database
                .SqlQueryRaw<NhaCC>("EXEC NhaCC_CT @MaNCC={0}", maNCC)
                .ToList()
                .FirstOrDefault();
        }

        public void NhaCC_Them(string tenNCC, string? dienThoai, string? email, string diaChi, string maXP)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhaCC_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenNCC", tenNCC));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", (object?)dienThoai ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Email", (object?)email ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", diaChi));
            cmd.Parameters.Add(new SqlParameter("@MaXP", maXP));
            Database.OpenConnection();
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public void NhaCC_Sua(short maNCC, string? dienThoai, string tenNCC, string? email, string diaChi, string maXP)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhaCC_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNCC", maNCC));
            cmd.Parameters.Add(new SqlParameter("@TenNCC", tenNCC));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", (object?)dienThoai ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Email", (object?)email ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", diaChi));
            cmd.Parameters.Add(new SqlParameter("@MaXP", maXP));
            Database.OpenConnection();
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public void NhaCC_Xoa(short maNCC)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC NhaCC_Xoa @MaNCC={0}", maNCC);
        }
        public DbSet<WebQLBH.Models.NhaCC> NhaCC { get; set; } = default!;
        // ====== PhongBan ======
        public List<PhongBan> PhongBan_DS()
        {
            return this.Database
                .SqlQueryRaw<PhongBan>("EXEC PhongBan_DS")
                .ToList();
        }

        public PhongBan? PhongBan_CT(byte maPB)
        {
            return this.Database
                .SqlQueryRaw<PhongBan>("EXEC PhongBan_CT @MaPB={0}", maPB)
                .ToList()
                .FirstOrDefault();
        }

        public void PhongBan_Them(string tenPB)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC PhongBan_Them @TenPB={0}", tenPB);
        }

        public void PhongBan_Sua(byte maPB, string tenPB)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC PhongBan_Sua @MaPB={0}, @TenPB={1}", maPB, tenPB);
        }

        public void PhongBan_Xoa(byte maPB)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC PhongBan_Xoa @MaPB={0}", maPB);
        }
        public DbSet<WebQLBH.Models.PhongBan> PhongBan { get; set; } = default!;
        public List<PhongBan> MaPB_DS()
        {
            return this.Database
                .SqlQueryRaw<PhongBan>("EXEC MaPB_DS")
                .ToList();
        }
        // ====== NhanVien ======
        public DbSet<NhanVien> NhanViens { get; set; }
        public List<NhanVien> NhanVien_DS()
        {
            var dsNhanVien = NhanViens.FromSqlRaw("EXEC NhanVien_DS").ToList();

            foreach (var item in dsNhanVien)
            {
                item.PhongBan = PhongBan_CT(item.MaPB);
            }
            return dsNhanVien;
        }
        public NhanVien NhanVien_CT(byte MaNV)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaNV", MaNV)
            };
            var nhanVien = NhanViens.FromSqlRaw("EXEC NhanVien_CT @MaNV", p).ToList().FirstOrDefault();
            nhanVien.PhongBan = PhongBan_CT(nhanVien.MaPB);
            return nhanVien;
        }
        public int NhanVien_Them(NhanVien nv)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Them";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@HoNV", nv.HoNV));
            cmd.Parameters.Add(new SqlParameter("@TenNV", nv.TenNV));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", nv.DienThoai));
            cmd.Parameters.Add(new SqlParameter("@Email", nv.Email));
            cmd.Parameters.Add(new SqlParameter("@TenDN", nv.TenDN));
            cmd.Parameters.Add(new SqlParameter("@AnhNV", nv.AnhNV ?? (object)DBNull.Value)); cmd.Parameters.Add(new SqlParameter("@MaPB", nv.MaPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_Sua(NhanVien nv)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Sua";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNV", nv.MaNV));
            cmd.Parameters.Add(new SqlParameter("@HoNV", nv.HoNV));
            cmd.Parameters.Add(new SqlParameter("@TenNV", nv.TenNV));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", nv.DienThoai));
            cmd.Parameters.Add(new SqlParameter("@Email", nv.Email));
            cmd.Parameters.Add(new SqlParameter("@TenDN", nv.TenDN));
            cmd.Parameters.Add(new SqlParameter("@AnhNV", nv.AnhNV ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaPB", nv.MaPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_Xoa(byte MaNV)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Xoa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNV", MaNV));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public byte NhanVien_DangNhap(string TenDN, string MatKhau) //Trả về Mã nhân viên nếu đăng nhập thành công
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_DangNhap";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            cmd.Parameters.Add(new SqlParameter("@MatKhau", MatKhau));
            Database.OpenConnection();
            var reader = cmd.ExecuteReader();
            byte maNV=0;
            if(reader.Read())//Nếu có dữ liệu để đọc
            {
                maNV = byte.Parse(reader["MaNV"].ToString());
            }
            Database.CloseConnection();
            return maNV;
        }
        public int NhanVien_DoiMatKhau(string TenDN, string MatKhauCu, string MatKhauMoi)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_DoiMatKhau";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            cmd.Parameters.Add(new SqlParameter("@MatKhauCu", MatKhauCu));
            cmd.Parameters.Add(new SqlParameter("@MatKhauMoi", MatKhauMoi));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_ResetMatKhau(string TenDN)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_ResetMatKhau";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        // ====== DonMH ======
        public List<DonMH> DonMH_DS()
        {
            return this.Database
                .SqlQueryRaw<DonMH>("EXEC DonMH_DS")
                .ToList();
        }

        public DonMH? DonMH_CT(string maDMH)
        {
            return this.Database
                .SqlQueryRaw<DonMH>("EXEC DonMH_CT @MaDMH={0}", maDMH)
                .ToList()
                .FirstOrDefault();
        }

        public void DonMH_Them(string maDMH, DateTime ngayDat, DateTime? ngayGiao, short maNCC)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonMH_Them @MaDMH={0}, @NgayDat={1}, @NgayGiao={2}, @MaNCC={3}",
                maDMH, ngayDat, ngayGiao ?? (object)DBNull.Value, maNCC);
        }

        public void DonMH_Sua(string maDMH, DateTime ngayDat, DateTime? ngayGiao, short maNCC)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonMH_Sua @MaDMH={0}, @NgayDat={1}, @NgayGiao={2}, @MaNCC={3}",
                maDMH, ngayDat, ngayGiao ?? (object)DBNull.Value, maNCC);
        }

        public void DonMH_Xoa(string maDMH)
        {
            this.Database.ExecuteSqlRaw(
                "EXEC DonMH_Xoa @MaDMH={0}", maDMH);
        }
        public DbSet<WebQLBH.Models.NhanVien> NhanVien { get; set; } = default!;
        public DbSet<WebQLBH.Models.DonMH> DonMH { get; set; } = default!;
        // ====== PhanQuyen ======
        public DbSet<PhanQuyen> PhanQuyen { get; set; }
        public List<PhanQuyen> PhanQuyen_DS()
        {
            var ds = PhanQuyen.FromSqlRaw("EXEC PhanQuyen_DS").ToList();
            foreach (var item in ds)
            {
                item.Quyen = Quyen_CT(item.MaQuyen);
                item.NhanVien = NhanVien_CT(item.MaNV);
            }
            return ds;
        }
        public PhanQuyen PhanQuyen_CT(byte maQuyen)
        {
            var p = new[] { new SqlParameter("@MaQuyen", maQuyen) };
            var pq = PhanQuyen.FromSqlRaw("EXEC PhanQuyen_CT @MaQuyen", p)
                .ToList().FirstOrDefault();
            if (pq != null)
            {
                pq.Quyen = Quyen_CT(pq.MaQuyen);
                pq.NhanVien = NhanVien_CT(pq.MaNV);
            }
            return pq;
        }
        public int PhanQuyen_Them(PhanQuyen pq)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhanQuyen_Them";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", pq.MaQuyen));
            cmd.Parameters.Add(new SqlParameter("@MaNV", pq.MaNV));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int PhanQuyen_Sua(PhanQuyen pq)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhanQuyen_Sua";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", pq.MaQuyen));
            cmd.Parameters.Add(new SqlParameter("@MaNV", pq.MaNV));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int PhanQuyen_Xoa(byte maQuyen)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhanQuyen_Xoa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", maQuyen));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        // ====== Quyen ======
        public DbSet<Quyen> Quyens { get; set; }
        public List<Quyen> Quyen_DS()
        {
            return Quyens.FromSqlRaw("EXEC Quyen_DS").ToList();
        }
        public Quyen Quyen_CT(byte maQuyen)
        {
            var p = new[] { new SqlParameter("@MaQuyen", maQuyen) };
            return Quyens.FromSqlRaw("EXEC Quyen_CT @MaQuyen", p)
                .ToList().FirstOrDefault();
        }
        public int Quyen_Them(Quyen q)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Quyen_Them";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", q.MaQuyen));
            cmd.Parameters.Add(new SqlParameter("@TenQuyen", q.TenQuyen));
            cmd.Parameters.Add(new SqlParameter("@MoTa", q.MoTa ?? (object)DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int Quyen_Sua(Quyen q)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Quyen_Sua";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", q.MaQuyen));
            cmd.Parameters.Add(new SqlParameter("@TenQuyen", q.TenQuyen));
            cmd.Parameters.Add(new SqlParameter("@MoTa", q.MoTa ?? (object)DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int Quyen_Xoa(byte maQuyen)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Quyen_Xoa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaQuyen", maQuyen));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        // ====== KhachHang ======
        public DbSet<KhachHang> KhachHangs { get; set; }
        public List<KhachHang> KhachHang_DS()
        {
            var ds = KhachHangs.FromSqlRaw("EXEC KhachHang_DS").ToList();
            var dsXP = XaPhuong_DS();
            foreach (var item in ds)
                item.XaPhuong = dsXP.FirstOrDefault(x => x.MaXP == item.MaXP?.Trim());
            return ds;
        }
        public KhachHang KhachHang_CT(short maKH)
        {
            var p = new[] { new SqlParameter("@MaKH", maKH) };
            var kh = KhachHangs.FromSqlRaw("EXEC KhachHang_CT @MaKH", p)
                .ToList().FirstOrDefault();
            if (kh != null)
                kh.XaPhuong = XaPhuong_CT(kh.MaXP!);
            return kh;
        }
        public int KhachHang_Them(KhachHang kh)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "KhachHang_Them";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@HoKH", kh.HoKH));
            cmd.Parameters.Add(new SqlParameter("@TenKH", kh.TenKH));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", kh.DienThoai ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Email", kh.Email ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", kh.DiaChi));
            cmd.Parameters.Add(new SqlParameter("@MaXP", kh.MaXP));
            cmd.Parameters.Add(new SqlParameter("@TenDN", kh.TenDN));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int KhachHang_Sua(KhachHang kh)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "KhachHang_Sua";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaKH", kh.MaKH));
            cmd.Parameters.Add(new SqlParameter("@HoKH", kh.HoKH));
            cmd.Parameters.Add(new SqlParameter("@TenKH", kh.TenKH));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", kh.DienThoai ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Email", kh.Email ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", kh.DiaChi));
            cmd.Parameters.Add(new SqlParameter("@MaXP", kh.MaXP));
            cmd.Parameters.Add(new SqlParameter("@TenDN", kh.TenDN));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int KhachHang_Xoa(short maKH)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "KhachHang_Xoa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaKH", maKH));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
    }
}
