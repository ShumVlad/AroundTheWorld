using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Create(CreateRouteDTO routeDTO, List<Location> locations);
        Task<bool> Delete(string id);
        Task<Route> Get(string id);
        Task<bool> Update(CreateRouteDTO route);
        Task<List<GetRouteDto>> GetUserRoutes(string userId);
        Task<List<GetRouteDto>> GetCompanyRoutes(string companyId);
    }
}
