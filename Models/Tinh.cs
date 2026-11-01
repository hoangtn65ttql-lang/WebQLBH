using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class Tinh
    {
        [Key]
        public string? MaTinh { get; set; }    // char(2)
        public string? TenTinh { get; set; }
    }
}