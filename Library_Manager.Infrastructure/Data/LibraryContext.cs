using Library_Manager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.Infrastructure.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}