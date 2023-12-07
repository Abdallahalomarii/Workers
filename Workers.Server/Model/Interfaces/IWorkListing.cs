using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IWorkListing
    {
        public Task<WorkListing> AddWorkListing(WorkListing workListing);

        public Task<List<WorkListing>> GetAllWorkListing();

        public Task<WorkListing> GetWorkLisitngById(int workListingId);

        public Task<WorkListing> UpdateWorkLisiting(int WorkListingId, WorkListing workListing);

        public Task DeleteWorkLisitng(int WorkListingId);
    }
}
