using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ISensorService
    {
        Task<bool> Add(Sensor sensor);
        Task<bool> Update(Sensor sensor);
        Task<List<Sensor>> GetAll();
    }
}
