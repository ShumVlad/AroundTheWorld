using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IUserPositionService
    {
        Task<bool> AddUserPosition(string UserId);
        Task<bool> Update(UserPositionDto userPosition);
    }
}
