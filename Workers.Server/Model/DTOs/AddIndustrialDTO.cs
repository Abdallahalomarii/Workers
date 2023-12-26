using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.DTOs
{
    public class PutAndAddIndustrialWorkerDTO
    {
        public string Name { get; set; }

        public string UserID { get; set; }

        public string? Location { get; set; }

        public string? PhoneNumber { get; set; }

        public int PricePerHour { get; set; }

        public bool IsActive { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between the 0 and the 10 float number accepted!")]
        public double? Rate { get; set; }
    }
}
