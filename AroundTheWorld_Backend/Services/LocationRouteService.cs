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
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILocationRouteExtraRepository _extraRepository;
        private ILocationService _locationService;

        public LocationRouteService(UnitOfWork unitOfWork, IMapper mapper, ILocationRouteExtraRepository locationRouteExtraRepository, ILocationService locationService)
        {
            _unitOfWork = unitOfWork;
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
            var locationExists = _unitOfWork.LocationRepository.Get(locationRouteDTO.LocationId);
            if (locationExists == null)
            {
                throw new ArgumentException("Invalid LocationId");
            }

            var routeExists =  _unitOfWork.RouteRepository.Get(locationRouteDTO.RouteId) != null;
            if (!routeExists)
            {
                throw new ArgumentException("Invalid RouteId");
            }
            LocationRoute locationRoute = _mapper.Map<LocationRoute>(locationRouteDTO);
            locationRoute.Id = Guid.NewGuid().ToString();
            await _unitOfWork.LocationRouteRepository.Add(locationRoute);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteLocationRoute(string id)
        {
            await _unitOfWork.LocationRouteRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public async Task<List<Location>> GetLocationsInRoute(string routeId)
        {
            List<string> locationsId = await _extraRepository.GetLocationIdsFromRoute(routeId);
            List<Location> locations = new List<Location>();
            for(int i = 0; i < locationsId.Count; i++)
            {
                locations.Add(_locationService.Get(locationsId[i]));
            }
            return locations;
        }
    }
}
