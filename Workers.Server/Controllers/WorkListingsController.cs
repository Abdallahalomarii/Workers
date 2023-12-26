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
    public class WorkListingsController : ControllerBase
    {
        private readonly IWorkListing _context;

        public WorkListingsController(IWorkListing context)
        {
            _context = context;
        }

        // GET: api/WorkListings
        [Authorize (Roles = "Admin Manager, Worker Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkListingDTO>>> GetWorkListings()
        {
         
            var works = await _context.GetAllWorkListing();
            if (works == null)
            {
                return NotFound();
            }
            
                return works;
        }

        // GET: api/WorkListings/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkListingDTO>> GetWorkListing(int id)
        {
            var workListing = await _context.GetWorkLisitngById(id);

            if (workListing == null)
            {
                return NotFound();
            }

            return workListing;
        }

        // PUT: api/WorkListings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkListing(int id, PutAndAddWorkListingDTO workListing)
        {
            var work = await _context.UpdateWorkLisiting(id, workListing);

            if (work != null)
            {
                return Ok(work);
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/WorkListings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //[Authorize(Roles = "Admin Manager")]
        [HttpPost]
        public async Task<ActionResult<WorkListingDTO>> PostWorkListing(PutAndAddWorkListingDTO workListing)
        {
            if (_context == null)
            {
                return Problem("Entity set 'WorkersDbContext.IndustrialWorkers'  is null.");
            }
            var postWork = await _context.AddWorkListing(workListing);

            return Ok(postWork);
        }

        // DELETE: api/WorkListings/5
        [Authorize(Roles = "Admin Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkListing(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteWorkLisitng(id);
            return Ok();
        }

       
    }
}
