using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.Models
{
    public class WorkListing
    {
        [Key]
        public int WorkID { get; set; }

        public int WorkerID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IndustrialWorker IndustrialWorker { get; set; }
    }
}
