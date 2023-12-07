using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class WorkshopService : IWorkshop
    {
        public Task<Workshop> AddWorkshop(Workshop workshop)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkShop(int workshopId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Workshop>> GetAllWorkshop()
        {
            throw new NotImplementedException();
        }

        public Task<Workshop> GetWrkshopById(int WorkshopId)
        {
            throw new NotImplementedException();
        }

        public Task<Workshop> UpdateWorkshop(int workshopId, Workshop workshop)
        {
            throw new NotImplementedException();
        }
    }
}
