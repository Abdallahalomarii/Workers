using System.ComponentModel.DataAnnotations;
using Workers.Server.Models;

namespace Workers.Server.Model.Models
{
    public class Review
    {
        public string UserID { get; set; }

        public int WorkshopID { get; set; }

        public string Commennt { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between the 0 and the 10 float number accepted!")]
        public int Rating { get; set; }

        public ApplicationUser User { get; set; }

        public Workshop Workshop { get; set; }
    }
}
