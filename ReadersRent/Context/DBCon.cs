using Microsoft.EntityFrameworkCore;
using ReadersRent.Model;

namespace ReadersRent.Context
{
    public class DBCon : DbContext
    {
        public DBCon(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Reader> Reader { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<ReaderBook> ReaderBook { get; set; }
    }
}
