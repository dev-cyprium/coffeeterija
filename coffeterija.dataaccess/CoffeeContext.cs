using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            //modelBuilder.Entity<Favorites>()
            //.Property(f => f.CreatedAt)
            //.HasDefaultValueSql("NOW()");

            //AutomaticDateCreation(modelBuilder);
            AutomaticDate<Coffee>(modelBuilder);
            AutomaticDate<CoffeePrice>(modelBuilder);
            AutomaticDate<Continent>(modelBuilder);
            AutomaticDate<OriginCountry>(modelBuilder);
            AutomaticDate<User>(modelBuilder);
        }

        private void AutomaticDate<T>(ModelBuilder modelBuilder)
            where T : Datable
        {
            modelBuilder.Entity<T>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("NOW()");
        }

        //private void AutomaticDateCreation(ModelBuilder modelBuilder)
        //{
        //    PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Public);
        //    MethodInfo method = typeof(ModelBuilder).GetMethod("Entity");
        //    foreach(var prop in propertyInfos)
        //    {
        //        Type t = prop.PropertyType.GetGenericArguments()[0];
        //        // TODO: check is type t derived from date
        //        method.MakeGenericMethod(t);
        //        object entity = method.Invoke(modelBuilder, null);

        //        typeof(EntityTypeBuilder<>)
        //            .MakeGenericType(t)
        //            .GetMethod("Property")
        //            .Invoke(entity, new object[] { "CreatedAt" });
        //    }
        //}
    }
}
