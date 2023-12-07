using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class WorkListingService : IWorkListing
    {
        public Task<WorkListing> AddWorkListing(WorkListing workListing)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkLisitng(int WorkListingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkListing>> GetAllWorkListing()
        {
            throw new NotImplementedException();
        }

        public Task<WorkListing> GetWorkLisitngById(int workListingId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkListing> UpdateWorkLisiting(int WorkListingId, WorkListing workListing)
        {
            throw new NotImplementedException();
        }
    }
}
