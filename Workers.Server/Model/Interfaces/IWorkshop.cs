using Workers.Server.Model.DTOs;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IWorkshop
    {
        public Task<WorkshopDTO> AddWorkshop(PutAndAddWorkshopDTO workshop);

        public Task<List<WorkshopDTO>> GetAllWorkshop(int workerId);

        public Task<WorkshopDTO> GetWorkshopsByID(int WorkshopId);

        public Task<WorkshopDTO> UpdateWorkshop(int workshopId, PutAndAddWorkshopDTO workshop);

        public Task DeleteWorkShop(int workshopId);

    }
}
