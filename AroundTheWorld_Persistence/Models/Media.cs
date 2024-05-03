using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Persistence.Models
{
    public class Media
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
