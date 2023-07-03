using BaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.Context
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) 
            : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;
    }
}
