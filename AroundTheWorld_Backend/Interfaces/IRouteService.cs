﻿using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IRouteService
    {
        Task<bool> Create(RouteDTO routeDTO, List<Location> locations);
        Task<bool> Delete(string id);
        Route Get(string id);
        Task<bool> Update(Route route);
        Task<List<GetRouteDto>> GetUserRoutes(string userId);
        Task<List<GetRouteDto>> GetCompanyRoutes(string companyId);
    }
}
