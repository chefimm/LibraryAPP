using System.ComponentModel.DataAnnotations;

namespace LibraryAPP.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? IsbnNo { get; set; }
        public string? KitapAdi { get; set; }
        public string? YazarAdi { get; set; }
        public int? SayfaSayisi { get; set; }
        public DateTime? YayinlanmaYili { get; set; }
        public bool KutuphanedeMi { get; set; }
        public List<BookReport>? BookReport { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
