using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Models;

namespace Workers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReview context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReivew(PutAndAddReviewDTO review)
        {
            if (_context == null)
            {
                return Problem("Entity set 'WorkersDbContext.Reviews'  is null.");
            }

            var postReview = await _context.AddReview(review, this.User);

            return Ok(postReview);
        }
        //TODO: Create a method to return the all reviews depend on a workship id.
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            if (_context == null)
            {
                return NotFound();
            }
            return await _context.GetAllReviews();
        }

        [HttpGet("{workshopId}")]
        public async Task<ActionResult<ReviewDTO>> getReview(int workshopId)
        {
            if (_context == null)
            {
                return NotFound();
            }

            var getUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(getUser);

            var review = await _context.GetReviewById(user.Id, workshopId);

            if (review == null)
            {
                return NotFound();
            }
            return review;
        }

        [HttpPut("{workshopId}")]
        public async Task<IActionResult> PutReview(int workshopId, PutAndAddReviewDTO review)
        {
            var getUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(getUser);

            var updateReview = await _context.UpdateReview(user.Id, workshopId,review);
            if (updateReview == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(updateReview);
            }
        }
    }
}
