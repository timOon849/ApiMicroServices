using ImageApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ImageApi.DB
{
    public class DBCon : DbContext
    {
        public DBCon(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Image> Image { get; set; }
    }
}
