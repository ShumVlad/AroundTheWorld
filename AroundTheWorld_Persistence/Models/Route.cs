using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class Route
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsFinisched { get; set; }
    }
}
