using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld_Persistence.Models
{
    public class RentItem
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
