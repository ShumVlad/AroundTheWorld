using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Models
{
    public class Trip
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
    }
}
