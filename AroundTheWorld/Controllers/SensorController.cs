using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Backend.Services;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<TaskStatus> Add(Sensor sensor)
        {
            if (sensor == null)
            {
                throw new ArgumentNullException(nameof(sensor));
            }
            var result = await _sensorService.Add(sensor);
            return result;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update(AroundTheWorld_Persistence.Models.Route sensor)
        {
            if (sensor == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _sensorService.Update(sensor);
            return result;
        }
    }
}
