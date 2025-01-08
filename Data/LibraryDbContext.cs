using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
    }
}
