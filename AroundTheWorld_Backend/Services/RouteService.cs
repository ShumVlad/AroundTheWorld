

using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace AroundTheWorld_Backend.Services
{
    public class RouteService : IRouteService
    {
        private UnitOfWork _unit;
        private IMapper _mapper;
        private ILocationRouteService _locationRouteService;

        public RouteService(UnitOfWork unitOfWork, IMapper mapper, ILocationRouteService locationRouteService)
        {
            _unit = unitOfWork;
            _mapper = mapper;
            _locationRouteService = locationRouteService;
        }

        public async Task<bool> Create(RouteDTO routeDTO, List<Location> locations)
        {
            if(routeDTO == null)
            {
                throw new ArgumentNullException(nameof(routeDTO));
            }
            var route = _mapper.Map<Route>(routeDTO);
            route.Id = Guid.NewGuid().ToString();
            await _unit.RouteRepository.Add(route);
            _unit.Save();
            LocationRouteDTO locationRouteouteDTO = new LocationRouteDTO();
            locationRouteouteDTO.IsVisited = false;
            locationRouteouteDTO.RouteId = route.Id;
            for (int i = 0; i < locations.Count; i++)
            {
                locationRouteouteDTO.LocationId = locations[i].Id;
                locationRouteouteDTO.Order = i+1;
                await _locationRouteService.AddLocationRoute(locationRouteouteDTO);
            }
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            await _unit.RouteRepository.Delete(id);
            _unit.Save();
            return true;
        }

        public Route Get(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Route result = _unit.RouteRepository.Get(id);
            return result;
        }

        public async Task<bool> Update(Route route)
        {
            if(route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }
            _unit.RouteRepository.Update(route);
            _unit.Save();
            return true;
        }

        public async Task<List<GetRouteDto>> GetUserRoutes(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            List<GetRoute> routes = await _unit.RouteRepository.GetUserRoutes(userId);
            List<GetRouteDto> getRouteDtos = _mapper.Map<List<GetRouteDto>>(routes);
            return getRouteDtos;
        }
        public async Task<List<GetRouteDto>> GetCompanyRoutes(string companyId)
        {
            if (companyId == null)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            List<GetRoute> routes = await _unit.RouteRepository.GetCompanyRoutes(companyId);
            List<GetRouteDto> getRouteDtos = _mapper.Map<List<GetRouteDto>>(routes);
            return getRouteDtos;
        }

    }
}
