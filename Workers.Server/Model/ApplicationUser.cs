using Microsoft.AspNetCore.Identity;
using Workers.Server.Model.Models;

namespace Workers.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }

        public IndustrialWorker IndustrialWorker { get; set; }
        public Review Review { get; set; }
    }
}
