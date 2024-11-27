using BookGenre.Model;
using Microsoft.EntityFrameworkCore;

namespace BookGenre.DB
{
    public class DBCon : DbContext
    {
        public DBCon(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}
