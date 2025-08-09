using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Persistence.Db
{
    public class AssetDbContextFactory : IDesignTimeDbContextFactory<AssetDbContext>
    {
        public AssetDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../AssetService.API"));
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AssetDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new AssetDbContext(optionsBuilder.Options);
        }
    }
}
