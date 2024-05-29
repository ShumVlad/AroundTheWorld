using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.DTOs
{
    public class UpdateSensorLocationDto
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
