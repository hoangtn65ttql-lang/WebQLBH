using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class KhachHang
    {
        [Key]
        public short MaKH { get; set; }
        public string? HoKH { get; set; }
        public string? TenKH { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? MaXP { get; set; }
        public string? TenDN { get; set; }
        public byte[]? MatKhau { get; set; }

        [NotMapped]
        public XaPhuong? XaPhuong { get; set; }
    }
}