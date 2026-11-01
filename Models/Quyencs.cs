using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class Quyen
    {
        [Key]
        public byte MaQuyen { get; set; }
        public string? TenQuyen { get; set; }
        public string? MoTa { get; set; }      // nullable
    }
}