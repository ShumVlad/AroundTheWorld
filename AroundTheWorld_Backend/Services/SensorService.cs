using AroundTheWorld_Backend.DTOs;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Services
{
    public class SensorService :ISensorService
    {
        private readonly UnitOfWork _unit;
        public SensorService(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<bool> Add(Sensor sensor)
        {
            sensor.Id = Guid.NewGuid().ToString();
            await _unit.SensorRepository.Add(sensor);
            _unit.Save();
            return true;
        }

        public async Task<bool> UpdateLocation(UpdateSensorLocationDto sensorDto)
        {
            Sensor sensor = await _unit.SensorRepository.Get(sensorDto.Id);
            sensor.Longitude = sensorDto.Longitude;
            sensor.Latitude = sensorDto.Latitude;
            sensor.Timestamp = sensorDto.Timestamp;
            await _unit.SensorRepository.Update(sensor);
            _unit.Save();
            return true;
        }

        public async Task<List<Sensor>> GetAll()
        {
            return await _unit.SensorRepository.GetAll();
        }
    }
}
