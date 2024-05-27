using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class SensorService
    {
        private readonly UnitOfWork _unit;
        public SensorService(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<TaskStatus> Add(Sensor sensor)
        {
            sensor.Id = Guid.NewGuid().ToString();
            var result = await (Task<TaskStatus>)_unit.SensorRepository.Add(sensor);
            _unit.Save();
            return result;
        }

        public async Task<bool> Update(Sensor sensor)
        {
            await _unit.SensorRepository.Update(sensor);
            _unit.Save();
            return true;
        }
    }
}
