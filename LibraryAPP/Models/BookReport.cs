using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPP.Models
{
    public class BookReport
    {
        [Key]
        public int Id { get; set; }
        public string? AlanKisiAdSoyad { get; set; }
        public string? AlanKisiTcNo { get; set; }
        public string? AlanKisiTelNo { get; set; }
        public DateTime? AlimTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public double? ToplamAlimSuresi { get; set; }
        public int? BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public bool IsDeleted { get; set; }
    }
}
