using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Create(CreateRouteDTO routeDTO);
        Task<bool> Delete(string id);
        Task<Route> Get(string id);
        Task<bool> Update(CreateRouteDTO route);
        Task<List<GetRoute>> GetAllRoutes(); 
        Task<List<GetRouteDto>> GetUserRoutes(string userId);
        Task<List<GetRouteDto>> GetCompanyRoutes(string companyId);
        Task<List<GetRouteDto>> GetNotUserRoutes(string userId);
    }
}
