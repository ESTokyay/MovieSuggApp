using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Context
{
    public class DbContextOptionGenerator
    {
        public static DbContextOptions<MovieContext> BuildOptions(IConfiguration _conf)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MovieSuggApp"))
                .AddJsonFile("appsettings.Development.json")
                .Build();            
            var connString = configuration.GetSection("MovieSuggDb").GetSection("ConnParam").Value;

            var builder = new DbContextOptionsBuilder<MovieContext>();
            builder.UseSqlServer(connString);
            return builder.Options;
            
        }
    }
}