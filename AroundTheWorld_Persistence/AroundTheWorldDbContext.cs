using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AroundTheWorld_Persistence
{
    public class AroundTheWorldDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Places { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Media> Medias { get; set; }

        public AroundTheWorldDbContext(DbContextOptions<AroundTheWorldDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            //AroundTheWorldDbContext.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
