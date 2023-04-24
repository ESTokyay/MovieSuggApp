using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MovieContext>
    {
        public DbContextFactory()
        {
            
        }
        public MovieContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MovieSuggApp"))
                .AddJsonFile("appsettings.Development.json")
                .Build();            
            var connString = configuration.GetSection("MovieSuggDb").GetSection("ConnParam").Value;

            var builder = new DbContextOptionsBuilder<MovieContext>();
            builder.UseSqlServer(connString);
            return new MovieContext(builder.Options);
        }
    }
}