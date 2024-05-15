using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace AroundTheWorld_Backend.Services
{
    public class LocationService : ILocationService
    {
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly AroundTheWorldDbContext _context;

        public LocationService(AroundTheWorldDbContext context, IMapper mapper, UnitOfWork unit)
        {
            _context = context;
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<bool> Update(Location location)
        {
            if (location == null)
            {
                return false;
            }
            await _unit.LocationRepository.Update(location);
            _unit.Save();
            return true;
        }

        public async Task<bool> Create(LocationDTO locationDTO)
        {
            if (locationDTO == null)
            {
                return true;
            }
            var location = _mapper.Map<Location>(locationDTO);
            location.Id = Guid.NewGuid().ToString();
            await _unit.LocationRepository.Add(location);
            _unit.Save();
            return true;
        } 

        public async Task<bool> Delete(string id)
        {
            if(id == null)
            {
                return false;
            }
            await _unit.LocationRepository.Delete(id);
            _unit.Save();
            return true;
        }

        public Location Get(string id)
        {
            Location result = _unit.LocationRepository.Get(id);
            return result;
        }
    }
}
