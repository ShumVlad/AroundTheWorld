using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
