using Workers.Server.Model.DTOs;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IWorkListing
    {
        public Task<WorkListingDTO> AddWorkListing(PutAndAddWorkListingDTO workListing);

        public Task<List<WorkListingDTO>> GetAllWorkListing();

        public Task<WorkListingDTO> GetWorkLisitngById(int workListingId);

        public Task<WorkListingDTO> UpdateWorkLisiting(int WorkListingId, PutAndAddWorkListingDTO workListing);

        public Task DeleteWorkLisitng(int WorkListingId);
    }
}
