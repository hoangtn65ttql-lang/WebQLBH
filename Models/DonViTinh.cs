using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class DonViTinh
    {
        [Key]
        public byte MaDVT { get; set; }
        public string? TenDVT { get; set; }
    }
}