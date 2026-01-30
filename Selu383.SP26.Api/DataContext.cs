using Microsoft.EntityFrameworkCore;

namespace Selu383.SP26.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().HasData(
    new Location { Id = 1, Name = "Seed", Address = "SeedAddr", TableCount = 5 }
);


        }
    }
}
