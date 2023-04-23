using System;
using System.IO;
using DATA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class MovieContext: DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MovieSuggApp"))
                .AddJsonFile("appsettings.Development.json")
                .Build();            
            var connString = configuration.GetSection("MovieSuggDb").GetSection("ConnParam").Value;

            optionsBuilder.UseSqlServer(connString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieEntity>().HasKey(x => x.EntityId);
            modelBuilder.Entity<Film>().HasKey(x => x.EntityId);
            modelBuilder.Entity<Notlar>().HasKey(x => x.EntityId);
            modelBuilder.Entity<Puanlar>().HasKey(x => x.EntityId);
            modelBuilder.Entity<Users>().HasKey(x => x.EntityId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<MovieEntity> MovieEntity { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Notlar> Notlar { get; set; }
        public DbSet<Puanlar> Puanlar { get; set; }
        public DbSet<Users> Kullanicilar{ get; set; }
    }
}