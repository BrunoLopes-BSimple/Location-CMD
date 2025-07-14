using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class LocationContextFactory : IDesignTimeDbContextFactory<LocationContext>
    {
        public LocationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocationContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Database=avaliacao-location-cmd-2;Username=postgres;Password=postgres");

            return new LocationContext(optionsBuilder.Options);
        }
    }
}