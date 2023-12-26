using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Workers.Server.Model.DTOs;
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
            builder.Entity<Review>()
         .HasKey(r => new { r.WorkshopID, r.UserID });

            builder.Entity<Review>()
                .HasOne(r => r.ApplicationUser)
                .WithOne(u => u.Review)
                .HasForeignKey<Review>(r => r.UserID)
                .OnDelete(DeleteBehavior.NoAction);  // Adjust the behavior as needed

            builder.Entity<Review>()
                .HasOne(r => r.Workshop)
                .WithMany(w => w.Reviews)
                .HasForeignKey(r => r.WorkshopID)
                .OnDelete(DeleteBehavior.NoAction);

            var hasher = new PasswordHasher<ApplicationUser>();

            var managerAdmin = new ApplicationUser
            {
                Id = "1a2e6f7b-dc4a-4c81-bbe2-8cd28f5e63f2",
                UserName = "AdminManager",
                NormalizedUserName = "ADMINMANAGER",
                Email = "adminmanager@example.com",
                PhoneNumber = "0772342313",
                NormalizedEmail = "adminmanager@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            managerAdmin.PasswordHash = hasher.HashPassword(managerAdmin, "ManagerAdmin@123");

            var WorkerAdmin = new ApplicationUser
            {
                Id = "3f9d28e4-6217-4a55-99b2-6e124c81f7c5",
                UserName = "WorkerAdmin",
                NormalizedUserName = "WORKERADMIN",
                Email = "workeradmin@example.com",
                PhoneNumber = "0781231251",
                NormalizedEmail = "workeradmin@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            WorkerAdmin.PasswordHash = hasher.HashPassword(WorkerAdmin, "WorkerAdmin@123");

            var userAdmin = new ApplicationUser
            {
                Id = "8b76a915-47c3-4f0d-98d6-9b284edaf7ae",
                UserName = "UserAdmin",
                NormalizedUserName = "USERADMIN",
                Email = "useradmin@example.com",
                PhoneNumber = "0791231541",
                NormalizedEmail = "useradmin@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            userAdmin.PasswordHash = hasher.HashPassword(userAdmin, "UserAdmin@123");

            builder.Entity<ApplicationUser>().HasData(managerAdmin);
            builder.Entity<ApplicationUser>().HasData(WorkerAdmin);
            builder.Entity<ApplicationUser>().HasData(userAdmin);

            List<IdentityUserRole<string>> identityUserRoles = new List<IdentityUserRole<string>>()
            {
            new IdentityUserRole<string> {UserId = "1a2e6f7b-dc4a-4c81-bbe2-8cd28f5e63f2", RoleId="admin manager"},
            new IdentityUserRole<string> { UserId = "3f9d28e4-6217-4a55-99b2-6e124c81f7c5", RoleId = "worker admin" },
            new IdentityUserRole<string> { UserId = "8b76a915-47c3-4f0d-98d6-9b284edaf7ae", RoleId ="user admin"}
            };

            builder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);

            seedRole(builder, "Admin Manager", "Create", "Update", "Delete", "Read");
            seedRole(builder, "Worker Admin", "Create", "Update", "Delete", "Read");
            seedRole(builder, "User Admin", "Create", "Update", "Delete", "Read");


            builder.Entity<IndustrialWorker>().HasData(
      new IndustrialWorker { ID = 1, UserID = "3f9d28e4-6217-4a55-99b2-6e124c81f7c5", Name = "John Doe", Location = "Jordan", PhoneNumber = "07833212312", IsActive = true, PricePerHour = 20, Rate = 8.5 }
      //new IndustrialWorker { ID = 2, UserID = "4f9d28e4-6217-4a55-99b2-6e124c81f7c6", Name = "Jane Doe", Location = "USA", PhoneNumber = "07833213456", IsActive = true, PricePerHour = 25, Rate = 9.0 },
      //new IndustrialWorker { ID = 3, UserID = "5f9d28e4-6217-4a55-99b2-6e124c81f7c7", Name = "Bob Smith", Location = "Canada", PhoneNumber = "07833214567", IsActive = true, PricePerHour = 18, Rate = 7.5 },
      //new IndustrialWorker { ID = 4, UserID = "6f9d28e4-6217-4a55-99b2-6e124c81f7c8", Name = "Alice Johnson", Location = "UK", PhoneNumber = "07833215678", IsActive = true, PricePerHour = 22, Rate = 8.8 },
      //new IndustrialWorker { ID = 5, UserID = "7f9d28e4-6217-4a55-99b2-6e124c81f7c9", Name = "Mike Wilson", Location = "Australia", PhoneNumber = "07833216789", IsActive = true, PricePerHour = 21, Rate = 8.3 },
      //new IndustrialWorker { ID = 6, UserID = "8f9d28e4-6217-4a55-99b2-6e124c81f7c0", Name = "Sara Miller", Location = "Germany", PhoneNumber = "07833217890", IsActive = true, PricePerHour = 24, Rate = 9.5 },
      //new IndustrialWorker { ID = 7, UserID = "9f9d28e4-6217-4a55-99b2-6e124c81f7c1", Name = "Tom Wilson", Location = "France", PhoneNumber = "07833218901", IsActive = true, PricePerHour = 19, Rate = 7.0 }
  );
            builder.Entity<WorkListing>().HasData(

                new WorkListing { ID = 1, IndustrialWorkerID = 1, Name = "Electrical", Description = "Electrical wiring and installation" },
                new WorkListing { ID = 2, IndustrialWorkerID = 1, Name = "Plumbing", Description = "Plumbing and pipe installation" },
                new WorkListing { ID = 3, IndustrialWorkerID = 1, Name = "Construction", Description = "General construction and building work" },
                new WorkListing { ID = 4, Name = "Painter", Description = "To paint the house walls" }
                );

            builder.Entity<Workshop>().HasData(
    new Workshop { ID = 1, IndustrialWorkerID = 1, Workshop_Name = "Electrical Workshop", Description = "Training and workshop for electrical work" },
    new Workshop { ID = 2, IndustrialWorkerID = 1, Workshop_Name = "Plumbing Workshop", Description = "Training and workshop for plumbing work" },
    new Workshop { ID = 3, IndustrialWorkerID = 1, Workshop_Name = "Construction Workshop", Description = "Training and workshop for construction work" },
    new Workshop { ID = 4, IndustrialWorkerID = 1, Workshop_Name = "Painter Workshop", Description = "Training and workshop for painting work" },
    new Workshop { ID = 5, IndustrialWorkerID = 1, Workshop_Name = "HVAC Workshop", Description = "Training and workshop for HVAC systems" },
    new Workshop { ID = 6, IndustrialWorkerID = 1, Workshop_Name = "Carpentry Workshop", Description = "Training and workshop for carpentry work" },
    new Workshop { ID = 7, IndustrialWorkerID = 1, Workshop_Name = "Masonry Workshop", Description = "Training and workshop for masonry work" }
);


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
