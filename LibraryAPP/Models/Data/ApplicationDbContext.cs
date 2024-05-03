using Microsoft.EntityFrameworkCore;

namespace LibraryAPP.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //performans için 
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookReport> BookReport { get; set; }
    }
}
