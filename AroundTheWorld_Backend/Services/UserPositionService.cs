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

        public UserPositionService(UnitOfWork unit)
        {
            _unit = unit;
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

        public async Task<bool> Update(UserPosition userPosition)
        {
            await _unit.UserPositionRepository.Update(userPosition);
            _unit.Save();
            return true;
        }
    }
}
