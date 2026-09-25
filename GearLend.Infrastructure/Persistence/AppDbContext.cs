using GearLend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace GearLend.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Sony FX3 Cinema Camera",
                    Category = "Camera",
                    SerialNumber = "SN-FX3-9901",
                    IsAvailable = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Asset
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Canon RF 24-70mm f/2.8L",
                    Category = "Lens",
                    SerialNumber = "SN-LNS-8802",
                    IsAvailable = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Asset
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Sennheiser Wireless Mic Set",
                    Category = "Audio",
                    SerialNumber = "SN-AUD-7703",
                    IsAvailable = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "John Doe", Email = "john@gearlend.com" },
                new User { Id = 2, FullName = "Jane Smith", Email = "jane@gearlend.com" }
            );
        }
    }
}
