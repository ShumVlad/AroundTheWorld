using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationRouteService
    {
        Task<bool> AddLocationRoute(LocationRouteDTO locationRouteDTO);
        Task<bool> DeleteLocationRoute(string id);
        Task<List<GetLocationFromRouteDto>> GetLocationsInRoute(string routeId);
    }
}
