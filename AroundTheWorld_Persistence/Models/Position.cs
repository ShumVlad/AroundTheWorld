using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
