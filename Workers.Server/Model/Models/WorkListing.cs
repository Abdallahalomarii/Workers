using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workers.Server.Model.Models
{
    public class WorkListing
    {
        public int ID { get; set; }

        public int IndustrialWorkerID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IndustrialWorker IndustrialWorker { get; set; }
    }
}
