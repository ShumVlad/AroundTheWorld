using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Route_Id { get; set; }

        [ForeignKey(nameof(Route_Id))]
        public virtual Route Route { get; set; }
    }
}
