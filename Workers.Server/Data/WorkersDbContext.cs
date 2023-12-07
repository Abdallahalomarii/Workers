using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workers.Server.Model.Models;
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
            base.OnModelCreating(builder);
            builder.Entity<Review>().HasKey(
                key => new
                {
                    key.UserID,
                    key.WorkshopID
                });
        }


        public DbSet<IndustrialWorker> IndustrialWorkers { get; set;}

        public DbSet<WorkListing>  WorkListings { get; set;}

        public DbSet<Workshop> Workshops { get; set;}

        public DbSet<Review> Reviews { get; set;}

    }
}
