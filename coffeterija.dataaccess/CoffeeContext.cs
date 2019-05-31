using System;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.dataaccess
{
    public class CoffeeContext : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<CoffeePrice> CoffePrices { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<OriginCountry> OriginCountries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dsn = @"Server=127.0.0.1;Port=5432;Database=coffeedb;User Id=postgres;Password=postgres";
            optionsBuilder.UseNpgsql(dsn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorites>()
                .HasKey(fav => new { fav.CoffeeId, fav.UserId });

            modelBuilder.Entity<Favorites>()
                .HasOne(fav => fav.Coffee)
                .WithMany(c => c.Favorites)
                .HasForeignKey(fav => fav.CoffeeId);

            modelBuilder.Entity<Favorites>()
                .HasOne(fav => fav.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(fav => fav.UserId);
        }

    }
}
