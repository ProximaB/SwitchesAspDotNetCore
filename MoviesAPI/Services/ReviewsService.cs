using System.Collections.Generic;
using System.Linq;
using MoviesAPI.DB.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Common;
using MoviesAPI.DB;

namespace MoviesAPI.Services
{
    public class ReviewsService : IReviewsService
    {        
        private readonly MoviesContext context;

        public ReviewsService(MoviesContext context)
        {   
            this.context = context;
        }

        public List<Review> GetAll()
        {
            return this.context.Reviews.ToList();
        }

        public List<Review> GetByMovieId(int movieId)
        {
            var movie = this.context.Movies.SingleOrDefault(m => m.Id == movieId);
            if(movie == null)
            {
                return null;
            }

            return movie.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            Review foundMovie = this.context.Reviews
                  .Where(review => review.Id == id)
                  .SingleOrDefault();

            return foundMovie;
        }

        public bool AddNewReview(Review review)
        {
            var movie = this.context.Movies.SingleOrDefault(m => m.Id == review.MovieId);
            if(movie == null)
            {
                return false;
            }

            movie.Reviews.Add(review);
            this.context.SaveChanges();

            return true;
        }

        public bool UpdateReview(Review review)
        {
            Review foundReview = GetById(review.Id);

            if (foundReview == null)
            {
                return false;
            }

            foundReview.Comment = review.Comment;
            foundReview.MovieId = review.MovieId;
            foundReview.Rate = review.Rate;
            context.SaveChanges();

            return true;
        }

        public void Remove(int reviewId)
        {
            Review review = GetById(reviewId);
            context.Reviews.Remove(review);
            context.SaveChanges();
        }
    }
}
