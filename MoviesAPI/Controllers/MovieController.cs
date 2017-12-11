using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DB.DbModels;
using MoviesAPI.Filters;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IRoomService _reviewsService;

        public MovieController(IMoviesService moviesService, IRoomService reviewsService)
        {
            _moviesService = moviesService;
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = AutoMapper.Mapper.Map<List<MovieResponse>>(_moviesService.GetAll());
            return Ok(movies);
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie if found</returns>
        [HttpGet("{movieId}")]
        [ExecutionTime]
        public IActionResult Get(int movieId)
        {
            Movie movie = _moviesService.GetById(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<MovieResponse>(movie));
        }


        [HttpGet("{movieId}/Reviews")]
        [ExecutionTime]
        public IActionResult GetReviews(int movieId)
        {
            var reviews = _reviewsService.GetByMovieId(movieId);
            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<List<ReviewResponse>>(reviews));
        }

        /// <summary>
        /// Add new movie to repositorium
        /// </summary>
        /// <param name="movie">new movie</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationAttribute]
        public IActionResult Post([FromBody]MovieRequest movie)
        {
            _moviesService.AddNewMovie(AutoMapper.Mapper.Map<Movie>(movie));

            return Ok();
        }

        /// <summary>
        /// Update movie in repositorium
        /// </summary>
        /// <param name="movie">updated movie</param>
        /// <returns></returns>
        [HttpPut("{movieId}")]
        public IActionResult Put(int movieId, [FromBody]MovieRequest movieRequest)
        {
            var movie = AutoMapper.Mapper.Map<Movie>(movieRequest);
            movie.Id = movieId;
            if (_moviesService.UpdateMovie(movie))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete movie from repositorium
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns></returns>
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            _moviesService.Remove(movieId);
            return Ok();
        }
    }
}
