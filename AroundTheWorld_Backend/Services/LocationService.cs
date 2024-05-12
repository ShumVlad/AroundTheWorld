using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class LocationService
    {
        private readonly UnitOfWork Unit;

        public async Task<string> Update(Location point)
        {
            await Unit.LocationRepository.Update(point);
            Unit.Save();
            return "Point is added to database";
        }

        public async Task<string> Create(Point point)
        {
            if (point is null)
            {
                return "Point is null";
            }
            point.Id = Guid.NewGuid().ToString();
            await Unit.PointRepository.Add(point);
            Unit.Save();
            return "Point is added to database";
        }
        public async Task<string> Delete(string id)
        {
            await Unit.PointRepository.Delete(id);
            Unit.Save();
            return "Point is deleted";
        }

        public Point Get(string id)
        {
            Point result = Unit.PointRepository.Get(id);
            return result;

        }
    }
}
