using F1_Car_Garage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F1_Car_Garage.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //colates and sets up the database connection string and other options for the context
            : base(options) { }

        public DbSet<Racer> Racers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<CarPart> CarParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // used to configure the model and relationships between entities in the database
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarPart>()
                .HasKey(cp => new { cp.CarId, cp.PartId });

            modelBuilder.Entity<Budget>()
                .Property(b => b.Spent)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Budget>()
                .Property(b => b.Sponsorship)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Budget>()
                .Property(b => b.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Part>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");
            // may change according to the environment as once the session is created it will be changed or deleted so this is just for testing purposes 
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ManufacturerId = 1, Name = "Ferrari", Country = "Italy", Tier = "Mid" },
                new Manufacturer { ManufacturerId = 2, Name = "Lambo", Country = "Italy", Tier = "Mid" },
                new Manufacturer { ManufacturerId = 3, Name = "Pagani", Country = "Italy", Tier = "Premium" }
            );
        }
    }
}