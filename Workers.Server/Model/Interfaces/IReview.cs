using System.Security.Claims;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IReview
    {
        public Task<ReviewDTO> AddReview(PutAndAddReviewDTO review,ClaimsPrincipal principal);

        public Task<List<ReviewDTO>> GetAllReviews();

        public Task<ReviewDTO> GetReviewById(string userID, int workshopId);

        public Task<ReviewDTO> UpdateReview(string userID, int workshopId, PutAndAddReviewDTO review);

        public Task DeleteReview(string userID, int workshopId);
    }
}
