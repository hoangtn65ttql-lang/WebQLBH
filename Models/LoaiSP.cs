using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebQLBH.Models;

namespace WebQLBH.Models
{
    public class LoaiSP
    {
        [Key]
        public short MaLSP { get; set; }
        public string TenLSP { get; set; }
        public byte MaNSP { get; set; }

        // Khai báo các bảng tham chiếu liên quan khóa ngoại
        [ForeignKey("MaNSP")] // Khai báo thuộc tính khóa ngoại
        [NotMapped]           
        public virtual NhomSP NhomSP { get; set; }
    }
}