using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Repositories.Interfaces
{
    public interface ILocationRouteExtraRepository
    {
        Task<List<LocationRoute>> GetLocationIdsFromRoute(string routeId);
    }
}
