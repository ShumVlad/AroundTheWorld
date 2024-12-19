using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ISensorService
    {
        Task<bool> Add(Sensor sensor);
        Task<bool> UpdateLocation(UpdateSensorLocationDto sensor);
        Task<List<Sensor>> GetAll();
    }
}
