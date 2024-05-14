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
        private readonly AroundTheWorldDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(AroundTheWorldDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Update(Location location)
        {
            if (location == null)
            {
                return "Location is null";
            }
            await _unit.LocationRepository.Update(location);
            _unit.Save();
            return "Location has been updated successfuly";
        }

        public async Task<bool> Create(LocationDTO locationDTO)
        {
            if (locationDTO == null)
            {
                return true;
            }
            var location = _mapper.Map<Location>(locationDTO);
            await _unit.LocationRepository.Add(location);
            _unit.Save();
            return true;
        } 

        public async Task<string> Delete(string id)
        {
            if(id == null)
            {
                return "Id is null";
            }
            await _unit.LocationRepository.Delete(id);
            _unit.Save();
            return "Location has been deleted successfuly";
        }

        public Location Get(string id)
        {
            Location result = _unit.LocationRepository.Get(id);
            return result;
        }
    }
}
