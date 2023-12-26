using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Workers.Server.Data;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;
using Workers.Server.Models;

namespace Workers.Server.Model.Services
{
    public class ReviewService : IReview
    {
        private readonly WorkersDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewService(WorkersDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ReviewDTO> AddReview(PutAndAddReviewDTO review, ClaimsPrincipal principal)
        {
            var getUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(getUserId);
            var newReview = new Review
            {
                UserID = user.Id,
                WorkshopID = review.WorkshopID,
                Comment = review.Comment,
                Rating = review.Rating,
            };
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();

            var returnedReviewRecord = await GetReviewById(newReview.UserID, newReview.WorkshopID);

            if (returnedReviewRecord != null)
            {
                return returnedReviewRecord;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteReview(string userID, int workshopId)
        {
            var review = await _context.Reviews.FindAsync(userID, workshopId);
            if (review != null)
            {
                 _context.Entry(review).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

            }
            
        }

        public async Task<List<ReviewDTO>> GetAllReviews()
        {
            var reviews = await _context.Reviews
                .Select(rv=> new ReviewDTO
                {
                    UserID = rv.UserID,
                    WorkshopID = rv.WorkshopID,
                    Comment = rv.Comment,
                    Rating = rv.Rating
                }).ToListAsync();
            if (reviews.Count == 0)
            {
                return null;
            }
            return reviews;
        }

        public async Task<ReviewDTO> GetReviewById(string userID, int workshopId)
        {
            var review = await _context.Reviews
                .Select(rv=> new ReviewDTO
                {
                    UserID = rv.UserID,
                    WorkshopID = rv.WorkshopID,
                    Comment = rv.Comment,
                    Rating = rv.Rating
                }).FirstOrDefaultAsync(keys => keys.UserID == userID && keys.WorkshopID == workshopId);
            if (review == null)
            {
                return null;
            }
            return review;
        }

        public async Task<ReviewDTO> UpdateReview(string userID, int workshopId, PutAndAddReviewDTO review)
        {
            var reviewToUpdate = await _context.Reviews.FindAsync(userID,workshopId);
            if (reviewToUpdate != null)
            {
                reviewToUpdate.UserID = userID;
                reviewToUpdate.WorkshopID = workshopId;
                reviewToUpdate.Comment = review.Comment;
                reviewToUpdate.Rating = review.Rating;

                _context.Entry(reviewToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            var returnReviewRecord = await GetReviewById(reviewToUpdate.UserID,reviewToUpdate.WorkshopID);
            if (returnReviewRecord != null)
            {
                return returnReviewRecord;
            }
            return null;
        }
    }
}
