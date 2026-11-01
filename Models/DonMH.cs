using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class DonMH
    {
        [Key]
        public string? MaDMH { get; set; }         // char(7)
        public DateTime NgayDat { get; set; }      // smalldatetime
        public DateTime? NgayGiao { get; set; }    // nullable
        public short MaNCC { get; set; }           // smallint, FK → NhaCC
    }
}