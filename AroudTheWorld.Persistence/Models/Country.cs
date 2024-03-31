using System.ComponentModel.DataAnnotations;

namespace AroudTheWorld.Persistence.Models
{
    public class Country
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double YCoordinate { get; set; }
        public double XCoordinate { get; set; }
        public string CapitalId { get; set; }

    }
}
