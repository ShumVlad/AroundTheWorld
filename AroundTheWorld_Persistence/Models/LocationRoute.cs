using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class LocationRoute
    {
        [Key]
        public string Id { get; set; }
        public int Order { get; set; }
        public bool IsVisited { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string RouteId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(RouteId))]
        public virtual Route Route { get; set; }
    }
}
