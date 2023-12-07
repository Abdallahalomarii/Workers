using Microsoft.AspNetCore.Identity;

namespace Workers.Server.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? Location { get; set; }

        public string? ImageUrl { get; set; }
        
    }
}
