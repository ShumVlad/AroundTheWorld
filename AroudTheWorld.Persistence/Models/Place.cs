using System.ComponentModel.DataAnnotations;

namespace AroudTheWorld.Persistence.Models
{
    public class Place
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
    }
}
