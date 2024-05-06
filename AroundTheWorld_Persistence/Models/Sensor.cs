using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Models
{
    public class Sensor
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Position_Id { get; set; }

        [ForeignKey(nameof(Position_Id))]
        public virtual Position Position { get; set; }
    }
}
