using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantDbContext : IdentityDbContext<User>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<User>()
                .HasMany(o => o.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(fk => fk.OwnerId);

            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);
            // Seed data in DbContext
            DataGenerator.Init(20);
            modelBuilder.Entity<Dish>().HasData(DataGenerator.Dishes);
            modelBuilder.Entity<Restaurant>().HasData(DataGenerator.Restaurants);
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address).HasData(DataGenerator.Addresses);
        }
    }
}