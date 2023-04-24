using DATA.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class MovieContext: DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        { }
        
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