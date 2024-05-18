using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationRouteService
    {
        Task<bool> CreateLocationRoute(LocationRoute locationRoute);
    }
}
