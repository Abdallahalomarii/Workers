using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.DTOs
{
    public class ReviewDTO
    {
        public string UserID { get; set; }
        
        public int WorkshopID { get; set; }

        public string? Comment { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between the 0 and the 10 float number accepted!")]
        public int? Rating { get; set; }
    }
}
