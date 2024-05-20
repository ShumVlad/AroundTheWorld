using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Repositories
{
    public class LocationRouteExtraRepository : ILocationRouteExtraRepository
    {
        private readonly AroundTheWorldDbContext _context;

        public LocationRouteExtraRepository(AroundTheWorldDbContext context)
        {
            _context = context;
        }

        public List<string> GetLocationIdsFromRoute(string routeId)
        {
            List<LocationRoute> locationRoutes = new List<LocationRoute>();

            var result = from uG in _context.LocationRoutes
                         where uG.RouteId.Equals(routeId)
                         select uG;
            locationRoutes = result.ToList();

            List<string> locationsId = new List<string>();
            for (int i = 0; i < locationRoutes.Count; i++)
            {
                locationsId.Add(locationRoutes[i].LocationId);
            }
            return locationsId;
        }

    }
}
