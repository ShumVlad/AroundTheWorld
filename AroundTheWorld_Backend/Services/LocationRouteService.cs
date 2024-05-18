

using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class LocationRouteService
    {
        private UnitOfWork _unitOfWork;
        private Mapper _mapper;

        LocationRouteService(UnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateLocationRoute(LocationRoute locationRoute)
        {
            if (locationRoute == null)
            {
                throw new ArgumentNullException(nameof(locationRoute));
            }
            locationRoute.Id = Guid.NewGuid().ToString();
            await _unitOfWork.LocationRouteRepository.Add(locationRoute);
            _unitOfWork.Save();
            return true;
        }
    }
}
