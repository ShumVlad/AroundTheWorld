using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld.ViewModels
{
    public class UpdateSensorLocationViewModel
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
