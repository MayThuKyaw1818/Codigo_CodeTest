using Microsoft.EntityFrameworkCore;

namespace POS_System.Model
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Point> Points { get; set; }
    }
}
