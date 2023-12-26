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
    public class WorkshopsController : ControllerBase
    {
        private readonly IWorkshop _context;

        public WorkshopsController(IWorkshop context)
        {
            _context = context;
        }

        // GET: api/Workshops
        //[Authorize(Roles = "Admin Manager")]
        [AllowAnonymous]
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<List<WorkshopDTO>>> GetWorkshops([FromRoute] int workerId)
        {

            var workshops = await _context.GetAllWorkshop(workerId);
            if (workshops == null)
            {
                return NotFound();
            }
            return workshops;
        }

        // GET: api/Workshops/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkshopDTO>> GetWorkshop(int id)
        {

            var workshop = await _context.GetWorkshopsByID(id);

            if (workshop == null)
            {
                return NotFound();
            }

            return workshop;
        }

        // PUT: api/Workshops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Worker Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop(int id, PutAndAddWorkshopDTO workshop)
        {

            var workshopToUpdate = await _context.UpdateWorkshop(id, workshop);

            if (workshopToUpdate != null)
            {
                return Ok(workshopToUpdate);
            }
            else
            {
                return NoContent();

            }
        }

        // POST: api/Workshops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Worker Admin")]
        [HttpPost]
        public async Task<ActionResult<WorkshopDTO>> PostWorkshop(PutAndAddWorkshopDTO workshop)
        {
            if (_context == null)
            {
                return Problem("Entity set 'WorkersDbContext.Workshops'  is null.");
            }
            var postWorkshop = await _context.AddWorkshop(workshop);
            if (postWorkshop != null)
            {
                return Ok(postWorkshop);
            }
            else
            {
                return NotFound();
            }
           
        }

        // DELETE: api/Workshops/5
        [Authorize(Roles = "Worker Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteWorkShop(id);
            return Ok();
        }

    }
}
