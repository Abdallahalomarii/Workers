using Workers.Server.Model.DTOs;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IIndustrialWorker
    {
        public Task<IndustrialWorkerDTO> AddIndustrialWorker(PutAndAddIndustrialWorkerDTO industrialWorker);

        public Task<List<IndustrialWorkerDTO>> GetAllIndustrialWorkers();

        public Task<IndustrialWorkerDTO> GetIndustrialWorkerById(int workerID);

        public Task<IndustrialWorkerDTO>  UpdateIndustrialWorker(int workerID, PutAndAddIndustrialWorkerDTO industrialWorker);

        public Task DeleteIndustrialWorker(int workerID);

    }
}
