using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationService
    {
        Task<bool> Create(LocationDTO locationDTO);
        Task<string> Delete(string id);
        Location Get(string id);
        Task<string> Update(Location location);
    }
}
