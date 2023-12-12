using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.Models
{
    public class Workshop
    {
        public int ID { get; set; }

        public int IndustrialWorkerID { get; set; }

        public string Workshop_Name { get; set; }

        public string Description { get; set; }

        public IndustrialWorker IndustrialWorker { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
