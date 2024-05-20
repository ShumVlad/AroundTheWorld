using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Repositories.Interfaces
{
    public interface ILocationRouteExtraRepository
    {
        List<string> GetLocationIdsFromRoute(string routeId);
    }
}
