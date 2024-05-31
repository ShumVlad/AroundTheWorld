using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ILocationService
    {
        Task<bool> Create(LocationDTO locationDTO);
        Task<bool> Delete(string id);
        Task<Location> Get(string id);
        Task<bool> Update(Location location);
        Task<List<Location>> GetPaginatedLocations(int page, int pageSize);
        Task<List<Location>> GetAll();
    }
}
