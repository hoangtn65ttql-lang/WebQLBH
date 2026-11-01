using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class NhaCC
    {
        [Key]
        public short MaNCC { get; set; }       // smallint = short
        public string? TenNCC { get; set; }
        public string? DienThoai { get; set; }  
        public string? Email { get; set; }     // nullable
        public string? DiaChi { get; set; }
        public string? MaXP { get; set; }      // char(5), FK → XaPhuong
    }
}