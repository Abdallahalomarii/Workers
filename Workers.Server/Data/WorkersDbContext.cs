using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
            

            seedRole(builder, "Admin Manager", "Create", "Update", "Delete", "Read");
            seedRole(builder, "Worker Admin", "Create", "Update", "Delete", "Read");
            seedRole(builder, "User Admin", "Create", "Update", "Delete", "Read");


        }

        int nextId = 1;
        private void seedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            var roleClaim = permissions.Select(permissions =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions",
                ClaimValue = permissions
            }).ToArray();
            modelBuilder.Entity<IdentityRole>().HasData(role);

        }


        public DbSet<IndustrialWorker> IndustrialWorkers { get; set; }

        public DbSet<WorkListing> WorkListings { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public DbSet<Review> Reviews { get; set; }

    }
}
