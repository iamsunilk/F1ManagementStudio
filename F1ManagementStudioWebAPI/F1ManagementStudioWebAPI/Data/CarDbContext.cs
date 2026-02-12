using F1ManagementStudioWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace F1ManagementStudioWebAPI.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Company> Companies { get; set; }

        }
}
