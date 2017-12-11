using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DB.DbModels;
using MoviesAPI.Filters;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IRoomService _reviewsService;

        public ReviewController(IRoomService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = AutoMapper.Mapper.Map<List<ReviewResponse>>(_reviewsService.GetAll());
            return Ok(reviews);
        }

        /// <summary>
        /// Get review by id
        /// </summary>
        /// <param name="reviewId">review id</param>
        /// <returns>Review if exist</returns>
        [HttpGet("{reviewId}")]
        public IActionResult Get(int reviewId)
        {
            Review review = _reviewsService.GetById(reviewId);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<ReviewResponse>(review));
        }

        /// <summary>
        /// Add new review
        /// </summary>
        /// <param name="review">Review</param>
        /// <returns></returns>
        [HttpPost]
        [MovieApiExceptionFilter]
        public IActionResult Post([FromBody]ReviewRequest review)
        {
            if (!_reviewsService.AddNewReview(AutoMapper.Mapper.Map<Review>(review)))
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Update review in repositorium
        /// </summary>
        /// <param name="review">updated review</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]ReviewRequest review)
        {
            if (_reviewsService.UpdateReview(AutoMapper.Mapper.Map<Review>(review)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete revie
        /// </summary>
        /// <param name="reviewId">review identifier</param>
        /// <returns></returns>
        [HttpDelete("{reviewId}")]
        public IActionResult Delete(int reviewId)
        {
            _reviewsService.Remove(reviewId);
            return Ok();
        }
    }
}
