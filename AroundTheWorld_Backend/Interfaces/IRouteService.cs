using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Delete(string id);
        Location Get(string id);
        Task<bool> Update(Route route);
    }
}
