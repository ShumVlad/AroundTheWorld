

using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace AroundTheWorld_Backend.Services
{
    public class RouteService : IRouteService
    {
        private UnitOfWork _unit;
        private IMapper _mapper;
        private ILocationRouteService _locationRouteService;
        private ILocationRouteExtraRepository _locationRouteExtraRepository;

        public RouteService(UnitOfWork unitOfWork, IMapper mapper, ILocationRouteService locationRouteService, ILocationRouteExtraRepository locationRouteExtraRepository)
        {
            _unit = unitOfWork;
            _mapper = mapper;
            _locationRouteService = locationRouteService;
            _locationRouteExtraRepository = locationRouteExtraRepository;
        }

        public async Task<bool> Create(CreateRouteDTO routeDTO)
        {
            Route route = _mapper.Map<Route>(routeDTO);
            route.Id = Guid.NewGuid().ToString();
            await _unit.RouteRepository.Add(route);
            _unit.Save();

            bool createGroupResult = await CreateGroup(routeDTO.GroupName, route.Id);
            if (!createGroupResult)
            {
                throw new GroupCreationException($"Failed to create group for route ID {route.Id} with group name {routeDTO.GroupName}.");
            }

            bool result = await AddLocationsToRoute(routeDTO.Locations, route.Id);
            return result;
        }

        public class GroupCreationException : Exception
        {
            public GroupCreationException(string message) : base(message) { }
        }

        public async Task<bool> CreateGroup(string name, string routeId) 
        {
            Group group = new Group();
            group.Id = Guid.NewGuid().ToString();
            group.Name = name;
            group.RouteId = routeId;
            await _unit.GroupRepository.Add(group);
            _unit.Save();
            return true;
        }

        public async Task<bool> AddLocationsToRoute(List<Location> locations, string routeId)
        {
            LocationRouteDTO locationRouteouteDTO = new LocationRouteDTO();
            locationRouteouteDTO.IsVisited = false;
            locationRouteouteDTO.RouteId = routeId;
            for (int i = 0; i < locations.Count; i++)
            {
                locationRouteouteDTO.LocationId = locations[i].Id;
                locationRouteouteDTO.Order = i + 1;
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

        public async Task<Route> Get(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Route result = await _unit.RouteRepository.Get(id);
            return result;
        }

        public async Task<bool> Update(CreateRouteDTO routeDTO)
        {
            if(routeDTO == null)
            {
                throw new ArgumentNullException(nameof(routeDTO));
            }
            Route route = _mapper.Map<Route>(routeDTO);
            await _unit.RouteRepository.Update(route);
            Group group = await _unit.GroupRepository.GetGroupByRouteId(routeDTO.Id);
            group.Name = routeDTO.GroupName;

            List<LocationRoute> oldLocationRoutes = await _locationRouteExtraRepository.GetLocationsFromRoute(routeDTO.Id);
            foreach (LocationRoute locationRoute in oldLocationRoutes)
            {
                _unit.LocationRouteRepository.Delete(locationRoute.Id);
            }
            _unit.Save();
            LocationRouteDTO locationRouteouteDTO = new LocationRouteDTO();
            locationRouteouteDTO.IsVisited = false;
            locationRouteouteDTO.RouteId = route.Id;
            for (int i = 0; i < routeDTO.Locations.Count; i++)
            {
                locationRouteouteDTO.LocationId = routeDTO.Locations[i].Id;
                locationRouteouteDTO.Order = i + 1;
                await _locationRouteService.AddLocationRoute(locationRouteouteDTO);
            }
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
