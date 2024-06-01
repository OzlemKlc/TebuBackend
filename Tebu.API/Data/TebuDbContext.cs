using Microsoft.EntityFrameworkCore;
using Tebu.API.Data.Models;

namespace Tebu.API.Data
{
    public class TebuDbContext : DbContext
    {
        public TebuDbContext(DbContextOptions<TebuDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(s => s.Addresses).WithOne(s => s.User).HasForeignKey(s => s.UserId);
                entity.HasMany(s => s.Vehicles).WithOne(s => s.User).HasForeignKey(s => s.UserId);
                entity.HasMany(s => s.CustomerOrders).WithOne(s => s.Costumer).HasForeignKey(s => s.CustomerId);
                entity.HasMany(s => s.WorkerOrders).WithOne(s => s.Worker).HasForeignKey(s => s.WorkerId);

                entity.HasIndex(s => s.PhoneNumber).IsUnique(true);
                entity.HasIndex(s => s.Email).IsUnique(true);

            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(s => s.Costumer).WithMany(s => s.CustomerOrders).HasForeignKey(s => s.CustomerId);
                entity.HasOne(S => S.Worker).WithMany(s => s.WorkerOrders).HasForeignKey(s => s.WorkerId);
                entity.HasOne(s => s.Vehicle).WithMany(s => s.Orders).HasForeignKey(s => s.VehicleId);
                entity.HasOne(S => S.Address).WithMany(S => S.Orders).HasForeignKey(S => S.AddressId);

            });
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(s => s.User).WithMany(s => s.Addresses).HasForeignKey(s => s.UserId);
                entity.HasMany(s => s.Orders).WithOne(s => s.Address).HasForeignKey(s => s.AddressId);
            });
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasOne(s => s.User).WithMany(s => s.Vehicles).HasForeignKey(s => s.UserId);
                entity.HasMany(s => s.Orders).WithOne(s => s.Vehicle).HasForeignKey(s => s.VehicleId);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
