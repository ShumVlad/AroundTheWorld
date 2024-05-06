using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AroundTheWorld_Persistence.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Position_Id { get; set; }
        [ForeignKey(nameof(Position_Id))]
        public virtual Position Position { get; set; }
    }
}
