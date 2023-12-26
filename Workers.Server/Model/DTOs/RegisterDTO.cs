using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string? Location { get; set; }

        public string? Roles { get; set; }
    }
}
