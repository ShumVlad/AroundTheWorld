using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class UserPositionService : IUserPositionService
    {
        private UnitOfWork _unit;
        private IMapper _mapper;

        public UserPositionService(UnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<bool> AddUserPosition(string userId)
        {
            UserPosition userPosition = new UserPosition();
            userPosition.Id = Guid.NewGuid().ToString();
            userPosition.UserId = userId;
            userPosition.Latitude = 0;
            userPosition.Longitude = 0;
            await _unit.UserPositionRepository.Add(userPosition);
            _unit.Save();
            return true;
        }

        public async Task<bool> Update(UserPositionDto userPosition)
        {
            UserPosition result = _mapper.Map<UserPosition>(userPosition);
            ApplicationUser user = await _unit.UserRepository.Get(userPosition.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid LocationId");
            }
            result.User = user;
            result.Id = await _unit.UserPositionRepository.GetUserPositionId(result.UserId);
            await _unit.UserPositionRepository.Update(result);
            _unit.Save();
            return true;
        }
    }
}
