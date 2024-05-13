using AroundTheWorld_Persistence.Models;

namespace AroundTheWorld_Backend.Services
{
    public class LocationService
    {
        private readonly UnitOfWork Unit;

        public async Task<string> Update(Location location)
        {
            if (location == null)
            {
                return "Location is null";
            }
            await Unit.LocationRepository.Update(location);
            Unit.Save();
            return "Location has been updated successfuly";
        }

        public async Task<string> Add(Location location)
        {
            if (location == null)
            {
                return "Location is null";
            }
            location.Id = Guid.NewGuid().ToString();
            await Unit.LocationRepository.Add(location);
            Unit.Save();
            return "Location has been added successfuly";
        } 

        public async Task<string> Delete(string id)
        {
            if(id == null)
            {
                return "Id is null";
            }
            await Unit.LocationRepository.Delete(id);
            Unit.Save();
            return "Location has been deleted successfuly";
        }

        public Location Get(string id)
        {
            Location result = Unit.LocationRepository.Get(id);
            return result;
        }
    }
}
