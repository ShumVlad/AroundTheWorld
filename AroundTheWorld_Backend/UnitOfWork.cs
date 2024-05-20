using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;

namespace AroundTheWorld_Backend
{
    public class UnitOfWork
    {
        private readonly AroundTheWorldDbContext _context;

        public readonly IRepository<Group> GroupRepository;
        public readonly IRepository<ApplicationUser> UserRepository;
        public readonly IRepository<Location> PlaceRepository;
        public readonly IRepository<Media> MediaRepository;
        public readonly IRepository<Location> LocationRepository;
        public readonly IRepository<Route> RouteRepository;
        public readonly IRepository<Sensor> SensorRepository;
        public readonly IRepository<Review> ReviewRepository;
        public readonly IRepository<Position> PositionRepository;
        public readonly IRepository<LocationRoute> LocationRouteRepository;
        public readonly IRepository<UserGroup> UserGroupRepository;
        public UnitOfWork(IRepository<Group> groupRepository, IRepository<Media> mediaRepository, IRepository<Location> placeRepository,
            IRepository<ApplicationUser> userRepository, IRepository<Route> routeRepository, IRepository<Sensor> sensorRepository,
            IRepository<Review> reviewRepository, IRepository<Position> positionRepository, IRepository<Location> locationRepository, 
            IRepository<LocationRoute> locationRouteRepository, IRepository<UserGroup> userGroupRepository, AroundTheWorldDbContext context)

        {
            PlaceRepository = placeRepository;
            PositionRepository = positionRepository;
            SensorRepository = sensorRepository;
            GroupRepository = groupRepository;
            ReviewRepository = reviewRepository;
            RouteRepository = routeRepository;
            MediaRepository = mediaRepository;
            UserRepository = userRepository;
            LocationRouteRepository = locationRouteRepository;
            LocationRepository = locationRepository;
            UserGroupRepository = userGroupRepository;
            _context = context; 
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}