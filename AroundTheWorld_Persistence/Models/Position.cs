using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class Position
    {
        [Key]
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
