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
        public async Task<AddIndustrialDTO> AddIndustrialWorker(AddIndustrialDTO industrialWorker)
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

            return industrialWorker;
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

        public async Task<List<IndustrialWorker>> GetAllIndustrialWorkers()
        {
            var workers = await _context.IndustrialWorkers.ToListAsync();
            if (workers.Count != 0)
            {
                return workers;
            }
            else
            {
                throw new Exception("There is no workers in the system!");
            }
        }

        public async Task<IndustrialWorker> GetIndustrialWorkerById(int workerID)
        {
            var worker = await _context.IndustrialWorkers.FindAsync(workerID);
            if (worker != null)
            {
                return worker;
            }
            else
            {
                throw new Exception("Worker id not found");

            }
        }

        public async Task<IndustrialWorker> UpdateIndustrialWorker(int workerID, IndustrialWorker industrialWorker)
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
                return worker;
            }
            else
            {
                throw new Exception("Update not completed because the worker is not found in the system!");
            }
        }
    }
}
