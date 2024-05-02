using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace AroundTheWorld_Backend
{
    internal class UnitOfWork
    {
        private readonly AroundTheWorldDbContext context;
        public readonly IRepository<City> CityRepository;
        public readonly IRepository<Country> CountryRepository;
        public readonly IRepository<ApplicationUser> UserRepository;
        public readonly IRepository<Location> PlaceRepository;
        public readonly IRepository<Post> PostRepository;
        public UnitOfWork(IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Location> placeRepository,
            IRepository<ApplicationUser> userRepository, IRepository<Post> postRepository)
        {
            PlaceRepository = placeRepository;
            CountryRepository = countryRepository;
            CityRepository = cityRepository;
            PostRepository = postRepository;
            UserRepository = userRepository;
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}