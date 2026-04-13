using Microsoft.EntityFrameworkCore;
using F1_Car_Garage.Models;

namespace F1_Car_Garage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Racer> Racers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<CarPart> CarParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarPart>()
                .HasKey(cp => new { cp.CarId, cp.PartId });

            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ManufacturerId = 1, Name = "Ferrari", Country = "Italy", Tier = "Premium" },
                new Manufacturer { ManufacturerId = 2, Name = "Pirelli", Country = "Italy", Tier = "Premium" },
                new Manufacturer { ManufacturerId = 3, Name = "Brembo", Country = "Italy", Tier = "Mid" }
            );
        }
    }
}