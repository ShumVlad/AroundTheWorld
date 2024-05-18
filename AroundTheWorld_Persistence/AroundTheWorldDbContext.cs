using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AroundTheWorld_Persistence
{
    public class AroundTheWorldDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<LocationRoute> LocationRoutes { get; set; }       

        public AroundTheWorldDbContext(DbContextOptions<AroundTheWorldDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
