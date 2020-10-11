using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Advantage.API.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApiContext>();

            var connectionString = configuration.GetConnectionString("NGsightDB");

            builder.UseSqlServer(connectionString);       

            return new ApiContext(builder.Options);
        }
    }
}