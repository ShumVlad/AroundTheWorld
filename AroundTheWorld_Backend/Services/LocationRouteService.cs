using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AroundTheWorld_Persistence.Repositories.Interfaces;
using AutoMapper;
using System.Text.RegularExpressions;

namespace AroundTheWorld_Backend.Services
{
    public class LocationRouteService : ILocationRouteService
    {
        private UnitOfWork _unit;
        private IMapper _mapper;
        private ILocationRouteExtraRepository _extraRepository;
        private ILocationService _locationService;

        public LocationRouteService(UnitOfWork unitOfWork, IMapper mapper, ILocationRouteExtraRepository locationRouteExtraRepository, ILocationService locationService)
        {
            _unit = unitOfWork;
            _mapper = mapper;
            _extraRepository = locationRouteExtraRepository;
            _locationService = locationService;
        }

        public async Task<bool> AddLocationRoute(LocationRouteDTO locationRouteDTO)
        {
            if (locationRouteDTO == null)
            {
                throw new ArgumentNullException(nameof(locationRouteDTO));
            }

            // Fetch the Location entity
            var location = await _unit.LocationRepository.Get(locationRouteDTO.LocationId);
            if (location == null)
            {
                throw new ArgumentException("Invalid LocationId");
            }

            var route = await _unit.RouteRepository.Get(locationRouteDTO.RouteId);
            if (route == null)
            {
                throw new ArgumentException("Invalid RouteId");
            }

            LocationRoute locationRoute = _mapper.Map<LocationRoute>(locationRouteDTO);
            locationRoute.Id = Guid.NewGuid().ToString();

            locationRoute.Location = location;
            locationRoute.Route = route;

            await _unit.LocationRouteRepository.Add(locationRoute);
            _unit.Save();
            return true;
        }


        public async Task<bool> DeleteLocationRoute(string id)
        {
            await _unit.LocationRouteRepository.Delete(id);
            _unit.Save();
            return true;
        }

        public async Task<List<GetLocationFromRouteDto>> GetLocationsInRoute(string routeId)
        {
            List<LocationRoute> locationRoutes = await _extraRepository.GetLocationsFromRoute(routeId);
            List<GetLocationFromRouteDto> getLocationFromRouteDtos = new List<GetLocationFromRouteDto>();

            foreach (var locationRoute in locationRoutes)
            {
                var location = await _unit.LocationRepository.Get(locationRoute.LocationId);
                if (location != null)
                {
                    var getLocationRoute = new GetLocationFromRouteDto
                    {
                        Longitude = location.Longitude,
                        Latitude = location.Latitude,
                        IsVisited = locationRoute.IsVisited,
                        Address = location.Address,
                        Description = location.Description,
                        Id = location.Id,
                        ImageUrl = location.ImageUrl,
                        Name = location.Name,
                        Order = locationRoute.Order,
                        Type = location.Type
                    };

                    getLocationFromRouteDtos.Add(getLocationRoute);
                }
            }

            return getLocationFromRouteDtos;
        }


        public async Task<List<Route>> GetRoutesWithLocation(string locationId)
        {
            List<Route> routes = await _unit.LocationRouteRepository.GetRoutesWithLocation(locationId);

            return routes;
        }
    }
}
