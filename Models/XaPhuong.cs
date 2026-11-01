using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class XaPhuong
    {
        [Key]
        public string? MaXP { get; set; }      // char(5)
        public string? TenXP { get; set; }
        public string? MaTinh { get; set; }    // char(2), FK → Tinh
    }
}