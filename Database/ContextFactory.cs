using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configuration =
                            new ConfigurationBuilder()
                            .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..")))
                            .AddJsonFile("appsettings.json", true)
                            .AddEnvironmentVariables()
                            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            _ = optionsBuilder.UseNpgsql(configuration["DATABASE_URL"]);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}