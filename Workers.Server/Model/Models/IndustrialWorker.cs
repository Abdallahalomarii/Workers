using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.Models
{
    public class IndustrialWorker
    {
        [Key]
        public int WorkerID { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Location { get; set; }

        public string Phone { get; set; }

        public int PricePerHour { get; set; }

        public bool IsActive { get; set; }

        [Range(0,10, ErrorMessage ="Rating must be between the 0 and the 10 float number accepted!")]
        public double Rate { get; set; }

        public ICollection<WorkListing> WorkListings { get; set; }

        public ICollection<Workshop> Workshops { get; set; }


    }
}
