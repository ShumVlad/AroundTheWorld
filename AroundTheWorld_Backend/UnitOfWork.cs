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
        public readonly IRepository<UserPosition> UserPositionRepository;
        public readonly IRepository<LocationRoute> LocationRouteRepository;
        public readonly IRepository<UserGroup> UserGroupRepository;
        public readonly IRepository<Company> CompanyRepository;
        public readonly IRepository<RentItem> RentItemRepository;
        public UnitOfWork(IRepository<Group> groupRepository, IRepository<Media> mediaRepository, IRepository<Location> placeRepository,
            IRepository<ApplicationUser> userRepository, IRepository<Route> routeRepository, IRepository<Sensor> sensorRepository,
            IRepository<Review> reviewRepository, IRepository<UserPosition> positionRepository, IRepository<Location> locationRepository, IRepository<RentItem> rentItemRepository,
            IRepository<LocationRoute> locationRouteRepository, IRepository<UserGroup> userGroupRepository, IRepository<Company> companyRepository, AroundTheWorldDbContext context)
        {
            PlaceRepository = placeRepository;
            UserPositionRepository = positionRepository;
            SensorRepository = sensorRepository;
            GroupRepository = groupRepository;
            ReviewRepository = reviewRepository;
            RouteRepository = routeRepository;
            MediaRepository = mediaRepository;
            UserRepository = userRepository;
            LocationRouteRepository = locationRouteRepository;
            LocationRepository = locationRepository;
            UserGroupRepository = userGroupRepository;
            CompanyRepository = companyRepository;
            RentItemRepository = rentItemRepository;
            _context = context; 
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}