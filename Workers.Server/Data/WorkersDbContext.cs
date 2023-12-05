using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workers.Server.Models;

namespace Workers.Server.Data
{
    public class WorkersDbContext : IdentityDbContext<ApplicationUser>
    {
        public WorkersDbContext(DbContextOptions option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
        }


    }
}
