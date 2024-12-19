using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class Sensor
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public string RentItemId { get; set; }

        [ForeignKey(nameof(RentItemId))]
        public virtual RentItem RentItem { get; set; }
    }
}
