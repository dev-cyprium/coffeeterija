using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace coffeterija.dataaccess
{
    public class CoffeeContext : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<CoffeePrice> CoffePrices { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<OriginCountry> OriginCountries { get; set; }
        public DbSet<User> Users { get; set; }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dsn = @"Server=127.0.0.1;Port=5432;Database=coffeedb;User Id=postgres;Password=postgres";
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseNpgsql(dsn);
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

            modelBuilder.Entity<CoffeeImage>()
                .HasOne(ci => ci.Coffee)
                .WithMany(cf => cf.Images)
                .HasForeignKey(ci => ci.CoffeeId);

            modelBuilder.Entity<User>()
                .HasIndex(field => field.Email)
                .IsUnique();

            AutomaticDate<Coffee>(modelBuilder);
            AutomaticDate<CoffeePrice>(modelBuilder);
            AutomaticDate<Continent>(modelBuilder);
            AutomaticDate<OriginCountry>(modelBuilder);
            AutomaticDate<User>(modelBuilder);
        }

        private void AutomaticDate<T>(ModelBuilder modelBuilder)
            where T : Datable
        {
            string sqlDefault = "NOW()";

            modelBuilder.Entity<T>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql(sqlDefault);

            modelBuilder.Entity<T>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValueSql(sqlDefault);
        }
    }
}
