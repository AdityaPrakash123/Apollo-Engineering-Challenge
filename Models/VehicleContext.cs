using Microsoft.EntityFrameworkCore;

namespace ApolloEngineeringChallenge.Models
{
    public class VehicleContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public VehicleContext(DbContextOptions options) : base(options)
        {

        }
    }
}
