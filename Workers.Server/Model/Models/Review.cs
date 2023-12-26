using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workers.Server.Models;

namespace Workers.Server.Model.Models
{
    public class Review
    {
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public int WorkshopID { get; set; }

        public string Comment { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public int Rating { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Workshop Workshop { get; set; }
    }
}
