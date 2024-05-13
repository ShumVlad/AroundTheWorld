using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class LocationRoute
    {
        [Key]
        public string Id { get; set; }
        public int Order { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string RouteId { get; set; }
    }
}
