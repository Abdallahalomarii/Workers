using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustrialWorkersController : ControllerBase
    {
        private readonly IIndustrialWorker _context;

        public IndustrialWorkersController(IIndustrialWorker context)
        {
            _context = context;
        }

        // GET: api/IndustrialWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndustrialWorker>>> GetIndustrialWorkers()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllIndustrialWorkers();
        }

        // GET: api/IndustrialWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IndustrialWorker>> GetIndustrialWorker(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var industrialWorker = await _context.GetIndustrialWorkerById(id);

            if (industrialWorker == null)
            {
                return NotFound();
            }

            return industrialWorker;
        }

        // PUT: api/IndustrialWorkers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndustrialWorker(int id, IndustrialWorker industrialWorker)
        {
            if (id != industrialWorker.WorkerID)
            {
                return BadRequest();
            }

            var worker = await _context.UpdateIndustrialWorker(id, industrialWorker);

           if (worker != null)
            {
                return Ok(worker);
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/IndustrialWorkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddIndustrialDTO>> PostIndustrialWorker(AddIndustrialDTO industrialWorker)
        {
          if (_context == null)
          {
              return Problem("Entity set 'WorkersDbContext.IndustrialWorkers'  is null.");
          }
            await _context.AddIndustrialWorker(industrialWorker);

            return Ok(industrialWorker);
        }

        // DELETE: api/IndustrialWorkers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustrialWorker(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteIndustrialWorker(id);
            return Ok();
        }
    }
}
