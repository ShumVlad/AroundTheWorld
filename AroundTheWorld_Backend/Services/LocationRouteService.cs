

using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class LocationRouteService : ILocationRouteService
    {
        private UnitOfWork _unitOfWork;
        private AroundTheWorldDbContext _context;

        public LocationRouteService(UnitOfWork unitOfWork)
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
