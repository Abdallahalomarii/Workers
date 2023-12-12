using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class IndustrialWorkersController : ControllerBase
    {
        private readonly IIndustrialWorker _context;

        public IndustrialWorkersController(IIndustrialWorker context)
        {
            _context = context;
        }

        // GET: api/IndustrialWorkers
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndustrialWorkerDTO>>> GetIndustrialWorkers()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllIndustrialWorkers();
        }

        // GET: api/IndustrialWorkers/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<IndustrialWorkerDTO>> GetIndustrialWorker(int id)
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
        [Authorize(Roles ="Admin Manager , Worker Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndustrialWorker(int id, PutAndAddIndustrialWorkerDTO industrialWorker)
        {
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
        // TODO: Add property to the worker which is the username to specify this worker and make realtion between the user and the worker!!
        [Authorize(Roles ="User Admin")]
        [HttpPost]
        public async Task<ActionResult<IndustrialWorkerDTO>> PostIndustrialWorker(PutAndAddIndustrialWorkerDTO industrialWorker)
        {
          if (_context == null)
          {
              return Problem("Entity set 'WorkersDbContext.IndustrialWorkers'  is null.");
          }
           var postWorker =  await _context.AddIndustrialWorker(industrialWorker);

            return Ok(postWorker);
        }

        // DELETE: api/IndustrialWorkers/5
        [Authorize(Roles = "Admin Manager , Worker Admin")]
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
