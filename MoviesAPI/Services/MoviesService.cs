using System.Collections.Generic;
using System.Linq;
using MoviesAPI.DB.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.DB;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesContext _context;

        public MoviesService(MoviesContext context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.Find(id);
        }

        public void AddNewMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public bool UpdateMovie(Movie movie)
        {
            Movie foundMovie = GetById(movie.Id);

            if (foundMovie == null)
            {
                return false;
            }

            foundMovie.Title = movie.Title;
            foundMovie.Year = movie.Year;

            _context.SaveChanges();

            return true;
        }

        public void Remove(int movieId)
        {
            Movie movie = GetById(movieId);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
