using WebAPIAutoLink.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAutoLink.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarStatus> CarsStatuss { get; set; }
        public DbSet<FleetOwner> FleetOwners { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(a => a.CarStatus)
                .WithOne(a => a.Car)
                .HasForeignKey<CarStatus>(c => c.CarId);

            modelBuilder.Entity<Location>().HasMany(location => location.Orders)
                 .WithOne().HasForeignKey(con => con.EndLocationId).OnDelete(DeleteBehavior.ClientSetNull); ;

            modelBuilder.Entity<Location>().HasMany(location => location.Orders)
                 .WithOne().HasForeignKey(con => con.StartLocationId).OnDelete(DeleteBehavior.ClientSetNull); ;

            modelBuilder.Entity<Car>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Order>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }
    }
}
