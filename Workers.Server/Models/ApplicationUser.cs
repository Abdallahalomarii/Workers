﻿using Microsoft.AspNetCore.Identity;

namespace Workers.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
    }
}