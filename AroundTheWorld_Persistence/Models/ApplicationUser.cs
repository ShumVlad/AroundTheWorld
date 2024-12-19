using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AroundTheWorld_Persistence.Models
{
    public class ApplicationUser : IdentityUser
    {
        [AllowNull]
        public string CompanyId { get; set; }
    }
}
