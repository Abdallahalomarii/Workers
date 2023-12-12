using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class IndustrialWorkerService : IIndustrialWorker
    {
        private readonly WorkersDbContext _context;

        public IndustrialWorkerService(WorkersDbContext context)
        {
            _context = context;
        }
        public async Task<IndustrialWorkerDTO> AddIndustrialWorker(PutAndAddIndustrialWorkerDTO industrialWorker)
        {

            var worker = new IndustrialWorker
            {
                Name = industrialWorker.Name,
                Location = industrialWorker.Location,
                Phone = industrialWorker.Phone,
                PricePerHour = industrialWorker.PricePerHour,
                Rate = industrialWorker.Rate,
                IsActive = industrialWorker.IsActive
            };

            await _context.IndustrialWorkers.AddAsync(worker);

            await _context.SaveChangesAsync();

            var returnedWorker = await GetIndustrialWorkerById(worker.ID);
            if (returnedWorker != null)
            {
                return returnedWorker;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteIndustrialWorker(int workerID)
        {
            var worker = await _context.IndustrialWorkers.FindAsync(workerID);
            if (worker != null)
            {
                _context.Entry(worker).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Worker id not found");
            }
        }

        public async Task<List<IndustrialWorkerDTO>> GetAllIndustrialWorkers()
        {
            var worker = await _context.IndustrialWorkers
                .Select(wk => new IndustrialWorkerDTO
                {
                    ID = wk.ID,
                    Name = wk.Name,
                    Location = wk.Location,
                    Phone = wk.Phone,
                    PricePerHour = wk.PricePerHour,
                    Rate = wk.Rate,
                    IsActive = wk.IsActive
                }).ToListAsync();
            if (worker.Count != 0)
            {
                return worker;
            }
            else
            {
                return null;
            }
        }

        public async Task<IndustrialWorkerDTO> GetIndustrialWorkerById(int workerID)
        {
            var worker = await _context.IndustrialWorkers
               .Where(wk => wk.ID == workerID)
               .Select(wk => new IndustrialWorkerDTO
               {
                   ID = wk.ID,
                   Name = wk.Name,
                   Location = wk.Location,
                   Phone = wk.Phone,
                   PricePerHour = wk.PricePerHour,
                   Rate = wk.Rate,
                   IsActive = wk.IsActive
               }).FirstOrDefaultAsync();
            if (worker != null)
            {
                return worker;
            }
            else
            {
                return null;
            }
        }

        public async Task<IndustrialWorkerDTO> UpdateIndustrialWorker(int workerID, PutAndAddIndustrialWorkerDTO industrialWorker)
        {
            var worker = await _context.IndustrialWorkers.FindAsync(workerID);
            if (worker != null)
            {
                worker.Name = industrialWorker.Name;
                worker.Location = industrialWorker.Location;
                worker.PricePerHour = industrialWorker.PricePerHour;
                worker.Rate = industrialWorker.Rate;
                worker.IsActive = industrialWorker.IsActive;
                worker.Phone = industrialWorker.Phone;

                _context.Entry(worker).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var returnedWorker = await GetIndustrialWorkerById(workerID);
                if (returnedWorker != null)
                {
                    return returnedWorker;
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
