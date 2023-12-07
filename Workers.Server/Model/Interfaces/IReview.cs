using Workers.Server.Model.Models;

namespace Workers.Server.Model.Interfaces
{
    public interface IReview
    {
        public Task<Review> AddReview(Review review);

        public Task<ICollection<Review>> GetAllReviews();

        public Task<Review> GetReviewById(int userID, int workshopId);

        public Task<Review> UpdateReview(int userId, int workshopId, Review review);

        public Task DeleteReview(int userId, int workshopId);
    }
}
