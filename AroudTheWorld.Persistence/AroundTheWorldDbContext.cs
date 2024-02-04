using AroudTheWorld.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AroudTheWorld.Persistence
{
    public class AroundTheWorldDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Post> Posts { get; set; }

        public AroundTheWorldDbContext(DbContextOptions<AroundTheWorldDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            //ApplicationDbContext.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
