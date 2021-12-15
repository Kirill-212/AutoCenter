using MicroServiceApp.InfrastructureLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceApp.ServiceRepository.ContextDB
{
    public class ContextDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ClientCar> ClientsCars { get; set; }
        public DbSet<Img> Imgs { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ActionCar> ActionsCars { get; set; }
        public DbSet<TestDrive> TestDrives { get; set; }

        public ContextDb(DbContextOptions<ContextDb> options): base(options)
        {
        Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(u => u.TotalCost).HasColumnType("money");
            modelBuilder.Entity<Car>().Property(u => u.Cost).HasColumnType("money");
            // modelBuilder.Entity<Employee>().HasKey(u => u.UserId);
            modelBuilder.Entity<Car>()
                 .HasIndex(u => u.VIN)
                 .IsUnique();
            modelBuilder.Entity<ClientCar>()
                 .HasIndex(u => u.RegisterNumber)
                 .IsUnique();
            modelBuilder.Entity<Role>()
                            .HasIndex(u => u.RoleName)
                            .IsUnique();
            modelBuilder.Entity<User>()
                            .HasIndex(u => u.Email)
                            .IsUnique();
        }
    }
}
