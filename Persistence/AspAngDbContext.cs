using AspAng.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AspAng.Persistence
{
    public class AspAngDbContext : DbContext
    {
        public DbSet<Make> Makes {get; set;}
        public DbSet<Feature> Features {get; set;}

        public DbSet<Vehicle> Vehicles {get; set;}

        public DbSet<Model> Models {get; set;}

        public AspAngDbContext(DbContextOptions<AspAngDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.VehicleId, vf.FeatureId});
            
        }
     
    }
}