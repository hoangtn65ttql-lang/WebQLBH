using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebQLBH.Models;
namespace WebQLBH.Models
{
    public class NhanVien
    {
        [Key]
        public byte MaNV { get; set; }
        public string HoNV { get; set; }
        public string TenNV { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string TenDN { get; set; }
        public byte[] MatKhau { get; set; }
        public string? AnhNV { get; set; }
        public byte MaPB { get; set; }
        //Khai báo các bảng tham chiếu liên quan khóa ngoại
        [ForeignKey("MaPB")]//Khai báo thuộc tính khóa ngoại
        public virtual PhongBan PhongBan { get; set; }
    }
}
