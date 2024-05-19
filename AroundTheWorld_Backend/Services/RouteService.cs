

using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AutoMapper;

namespace AroundTheWorld_Backend.Services
{
    public class RouteService : IRouteService
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
    }
}
