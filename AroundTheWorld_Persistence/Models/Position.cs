using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class Position
    {
        [Key]
        public string Id { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
    }
}
