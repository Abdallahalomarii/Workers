using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IWorkshop
    {
        public Task<Workshop> AddWorkshop(Workshop workshop);

        public Task<ICollection<Workshop>> GetAllWorkshop();

        public Task<Workshop> GetWrkshopById(int WorkshopId);

        public Task<Workshop> UpdateWorkshop(int workshopId, Workshop workshop);

        public Task DeleteWorkShop(int workshopId);

    }
}
