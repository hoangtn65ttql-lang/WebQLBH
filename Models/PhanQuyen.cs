using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class PhanQuyen
    {
        [Key]
        public byte MaQuyen { get; set; }
        public byte MaNV { get; set; }

        [ForeignKey("MaQuyen")]
        public virtual Quyen? Quyen { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien? NhanVien { get; set; }
    }
}