using AroudTheWorld.Persistence;
using AroudTheWorld.Persistence.Models;
using AroudTheWorld.Persistence.Repositories;

namespace AroundTheWorld.Backend
{
    internal class UnitOfWork
    {
        private readonly AroundTheWorldDbContext context;
        public readonly IRepository<City> CityRepository;
        public readonly IRepository<Country> CountryRepository;
        public readonly IRepository<ApplicationUser> UserRepository;
        public readonly IRepository<Place> PlaceRepository;
        public readonly IRepository<Post> PostRepository;

        public UnitOfWork(IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Place> placeRepository,
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
