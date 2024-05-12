using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories;

namespace AroundTheWorld_Backend
{
    internal class UnitOfWork
    {
        private readonly AroundTheWorldDbContext context;
        public readonly IRepository<Group> GroupRepository;
        public readonly IRepository<ApplicationUser> UserRepository;
        public readonly IRepository<Location> PlaceRepository;
        public readonly IRepository<Media> MediaRepository;
        public readonly IRepository<Location> LocationRepository;
        public readonly IRepository<Route> RouteRepository;
        public readonly IRepository<Sensor> SensorRepository;
        public readonly IRepository<Review> ReviewRepository;
        public readonly IRepository<Position> PositionRepository;
        public UnitOfWork(IRepository<Group> groupRepository, IRepository<Media> mediaRepository, IRepository<Location> placeRepository,
            IRepository<ApplicationUser> userRepository, IRepository<Route> routeRepository, IRepository<Sensor> sensorRepository,
            IRepository<Review> reviewRepository, IRepository<Position> positionRepository, IRepository<Location> locationRepository)
        {
            PlaceRepository = placeRepository;
            PositionRepository = positionRepository;
            SensorRepository = sensorRepository;
            GroupRepository = groupRepository;
            ReviewRepository = reviewRepository;
            RouteRepository = routeRepository;
            MediaRepository = mediaRepository;
            UserRepository = userRepository;
            LocationRepository = locationRepository;
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}