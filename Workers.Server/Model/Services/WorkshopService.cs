using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class WorkshopService : IWorkshop
    {
        private readonly WorkersDbContext _context;

        public WorkshopService(WorkersDbContext context)
        {
            _context = context;
        }
        public async Task<WorkshopDTO> AddWorkshop(PutAndAddWorkshopDTO workshop)
        {
            var newWorkshop = new Workshop
            {
                IndustrialWorkerID = workshop.IndustrialWorkerID,
                Workshop_Name = workshop.Workshop_Name,
                Description = workshop.Description
            };
            await _context.Workshops.AddAsync(newWorkshop);
            await _context.SaveChangesAsync();
            var returnedWorkshop = await GetWorkshopsByID(newWorkshop.ID);
            if (returnedWorkshop != null)
            {
                return returnedWorkshop;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteWorkShop(int workshopId)
        {
            var workshop = await _context.Workshops.FindAsync(workshopId);
            if (workshop != null)
            {
                _context.Entry(workshop).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
         
        }

        public async Task<List<WorkshopDTO>> GetAllWorkshop()
        {
            var workshops = await _context.Workshops
                .Select(wks => new WorkshopDTO
                {
                    ID = wks.ID,
                    IndustrialWorkerID = wks.IndustrialWorkerID,
                    Workshop_Name= wks.Workshop_Name,
                    Description = wks.Description
                }).ToListAsync();
            if (workshops.Count > 0)
            { return workshops; }
            else
            {
                return null;
            }
        }

        public async Task<WorkshopDTO> GetWorkshopsByID(int WorkshopId)
        {
            var workshop = await _context.Workshops
               .Select(wks => new WorkshopDTO
               {
                   ID = wks.ID,
                   IndustrialWorkerID = wks.IndustrialWorkerID,
                   Workshop_Name = wks.Workshop_Name,
                   Description = wks.Description
               }).FirstOrDefaultAsync(x=> x.ID == WorkshopId);

            if (workshop != null)
            { return workshop; }
            else
            {
                return null;
            }
        }

        public async Task<WorkshopDTO> UpdateWorkshop(int workshopId, PutAndAddWorkshopDTO workshop)
        {
            var updateWorkshop = await _context.Workshops.FindAsync(workshopId);
            if (updateWorkshop != null)
            {
                updateWorkshop.IndustrialWorkerID = workshop.IndustrialWorkerID;
                updateWorkshop.Workshop_Name = workshop.Workshop_Name;
                updateWorkshop.Description = workshop.Description;

                _context.Entry(updateWorkshop).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var returneWorkshopRecord = await GetWorkshopsByID(updateWorkshop.ID);
                if (returneWorkshopRecord != null)
                {
                    return returneWorkshopRecord;
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
