using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.Models
{
    public class Workshop
    {
        [Key]
        public int WorkshopID { get; set; }

        public int WorkerID { get; set; }

        public string Workshop_Name { get; set; }

        public string Description { get; set; }

        public IndustrialWorker industrialWorker { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
