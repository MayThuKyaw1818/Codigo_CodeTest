using Microsoft.EntityFrameworkCore;

namespace POS_System.Entity
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Point> Points { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}
