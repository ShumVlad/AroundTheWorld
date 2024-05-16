

using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class RouteService
    {
        private UnitOfWork _unit;
        private IMapper _mapper;

        public RouteService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unit = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(RouteDTO routeDTO)
        {
            if(routeDTO == null)
            {
                throw new ArgumentNullException(nameof(routeDTO));
            }
            var route = _mapper.Map<Route>(routeDTO);
            route.Id = Guid.NewGuid().ToString();
            await _unit.RouteRepository.Add(route);
            _unit.Save();
            return true;
        }
    }
}
