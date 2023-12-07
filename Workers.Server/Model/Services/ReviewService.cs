using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Models;

namespace Workers.Server.Model.Services
{
    public class ReviewService : IReview
    {
        public Task<Review> AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReview(int userId, int workshopId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Review>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int userID, int workshopId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateReview(int userId, int workshopId, Review review)
        {
            throw new NotImplementedException();
        }
    }
}
