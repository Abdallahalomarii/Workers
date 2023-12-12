using System.ComponentModel.DataAnnotations;

namespace Workers.Server.Model.DTOs
{
    public class WorkshopDTO
    {
        public int ID { get; set; }

        public int IndustrialWorkerID { get; set; }

        public string Workshop_Name { get; set; }

        public string Description { get; set; }
    }
}
