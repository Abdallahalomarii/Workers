using Workers.Server.Model.DTOs;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IIndustrialWorker
    {
        public Task<AddIndustrialDTO> AddIndustrialWorker(AddIndustrialDTO industrialWorker);

        public Task<List<IndustrialWorker>> GetAllIndustrialWorkers();

        public Task<IndustrialWorker> GetIndustrialWorkerById(int workerID);

        public Task<IndustrialWorker>  UpdateIndustrialWorker(int workerID, IndustrialWorker industrialWorker);

        public Task DeleteIndustrialWorker(int workerID);

    }
}
