using System;
using System.Reflection;
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

            modelBuilder.Entity<User>()
                .HasIndex(field => field.Email)
                .IsUnique();

            RegisterAllDates(modelBuilder);
        }

        private void RegisterAllDates(ModelBuilder modelBuilder)
        {   
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public);
            foreach(var property in properties)
            {
                Type t = property.GetType();
                MethodInfo method = GetType().GetMethod("AutomaticDate");
                method.MakeGenericMethod(t.GetGenericArguments()[0]);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }

        #pragma warning disable IDE0051 // Remove unused private members
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
