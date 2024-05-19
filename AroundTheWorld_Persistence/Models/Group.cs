using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class Group
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Route_Id { get; set; }

        [ForeignKey(nameof(Route_Id))]
        public virtual Route Route { get; set; }
    }
}
