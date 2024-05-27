using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface ISensorService
    {
        Task<TaskStatus> Add(Sensor sensor);
        Task<bool> Update(Sensor sensor);
    }
}
