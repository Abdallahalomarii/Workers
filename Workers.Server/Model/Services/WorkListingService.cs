using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class WorkListingService : IWorkListing
    {
        private readonly WorkersDbContext _context;

        public WorkListingService(WorkersDbContext context)
        {
            _context = context;
        }

        public async Task<WorkListingDTO> AddWorkListing(PutAndAddWorkListingDTO workListing)
        {
            var work = new WorkListing
            {
                IndustrialWorkerID = workListing.IndustrialWorkerID,
                Name = workListing.Name,
                Description = workListing.Description
            };
            await _context.WorkListings.AddAsync(work);
            await _context.SaveChangesAsync();

            var returnedWork = await GetWorkLisitngById(work.ID);
            if (returnedWork != null)
            {
                return returnedWork;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteWorkLisitng(int WorkListingId)
        {
            var work = await _context.WorkListings.FindAsync(WorkListingId);
            if (work != null)
            {
                _context.Entry(work).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkListingDTO>> GetAllWorkListing()
        {
            var works = await _context.WorkListings
                .Select(wk => new WorkListingDTO
                {
                    ID = wk.ID,
                    IndustrialWorkerID = wk.IndustrialWorkerID,
                    Name = wk.Name,
                    Description = wk.Description,
                }).ToListAsync();

            return works;
        }

        public async Task<WorkListingDTO> GetWorkLisitngById(int workListingId)
        {
            var work = await _context.WorkListings
                .Select(wk=> new WorkListingDTO
                {
                    ID= wk.ID,
                    IndustrialWorkerID = wk.IndustrialWorkerID,
                    Name = wk.Name,
                    Description = wk.Description
                })
                .FirstOrDefaultAsync(key=> key.ID == workListingId);
            if (work != null)
            {
                return work;
            }
            else
            {
                return null;
            }
        }

        public async Task<WorkListingDTO> UpdateWorkLisiting(int WorkListingId, PutAndAddWorkListingDTO workListing)
        {
            var work = await _context.WorkListings.FindAsync(WorkListingId);
            if (work != null)
            {
                work.IndustrialWorkerID = workListing.IndustrialWorkerID;
                work.Name = workListing.Name;
                work.Description = workListing.Description;

                _context.Entry(work).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var returnedWorkRecord = await GetWorkLisitngById(work.ID);
                if (returnedWorkRecord != null)
                {
                    return returnedWorkRecord;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
