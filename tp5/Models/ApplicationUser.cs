using Microsoft.AspNetCore.Identity;

namespace tp5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
