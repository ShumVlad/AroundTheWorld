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

        public async Task<List<string>> GetLocationIdsFromRoute(string routeId)
        {
            List<string> locationsIds = new List<string>();

            var result = from uG in _context.LocationRoutes
                         where uG.RouteId.Equals(routeId)
                         select uG.LocationId;
            locationsIds = result.ToList();
            return locationsIds;
        }

    }
}
