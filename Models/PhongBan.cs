using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class PhongBan
    {
        [Key]
        public byte MaPB { get; set; }
        public string? TenPB { get; set; }
    }
}