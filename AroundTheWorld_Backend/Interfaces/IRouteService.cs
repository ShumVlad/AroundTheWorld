using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Create(RouteDTO routeDTO);
        Task<bool> Delete(string id);
        Route Get(string id);
        Task<bool> Update(Route route);
        Task<List<GetRouteDto>> GetMyRoutes(string userId);
    }
}
