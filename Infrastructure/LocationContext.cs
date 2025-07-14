using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class LocationContext : DbContext
{
    public virtual DbSet<LocationDataModel> Locations { get; set; }

    public LocationContext(DbContextOptions<LocationContext> options) : base(options) { }
}
