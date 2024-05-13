using AroundTheWorld_Persistence.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationService
    {
        Task<string> Create(Location location);
        Task<string> Delete(string id);
        Location Get(string id);
        Location Update(Location location);
    }
}
