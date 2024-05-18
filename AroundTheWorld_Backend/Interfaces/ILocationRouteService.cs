using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationRouteService
    {
        Task<bool> CreateLocationRoute(LocationRouteDTO locationRouteDTO);
        Task<bool> DeleteLocationRoute(string id);
    }
}
