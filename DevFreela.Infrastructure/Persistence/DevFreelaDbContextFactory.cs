using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    internal class DevFreelaDbContextFactory : IDesignTimeDbContextFactory<DevFreelaDbContext>
    {
        public DevFreelaDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                 .AddJsonFile("appsettings.json")
                                                                 .AddUserSecrets<DevFreelaDbContextFactory>()
                                                                 .Build();

            var connectionString = configurationBuilder.GetConnectionString("DevFreelaCs");

            var optionsBuilder = new DbContextOptionsBuilder<DevFreelaDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DevFreelaDbContext(optionsBuilder.Options);
        }
    }
}
