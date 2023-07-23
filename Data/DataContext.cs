using Microsoft.EntityFrameworkCore;

namespace MyAPINetCore6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<MyHero> Heroes { get; set; }
    }
}
