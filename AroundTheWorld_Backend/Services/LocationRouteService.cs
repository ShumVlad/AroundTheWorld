using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class LocationRouteService : ILocationRouteService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LocationRouteService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
