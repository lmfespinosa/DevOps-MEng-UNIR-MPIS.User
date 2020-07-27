#region "Libraries"

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

#endregion

namespace MPIS.User.RepositoryModel
{
    class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            var connectionString = configuration["AzureSQL:ConnectionString"];

            optionsBuilder.UseSqlServer(connectionString, contextOptionsBuilder =>
            {
                contextOptionsBuilder.MigrationsHistoryTable("Migrations", "EF");
                contextOptionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            return new Context(optionsBuilder.Options);
        }
    }
}
