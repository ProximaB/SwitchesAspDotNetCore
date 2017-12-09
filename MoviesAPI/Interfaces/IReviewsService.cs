using System.Collections.Generic;
using MoviesAPI.DB.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IReviewsService
    {
        List<Review> GetAll();

        Review GetById(int id);

        List<Review> GetByMovieId(int movieId);

        bool AddNewReview(Review review);

        bool UpdateReview(Review review);

        void Remove(int reviewId);
    }
}
