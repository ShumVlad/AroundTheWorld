using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationRouteService
    {
        Task<bool> CreateLocationRoute(LocationRoute locationRoute);
    }
}
