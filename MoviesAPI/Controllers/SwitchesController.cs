using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class SwitchController : Controller
    {
        private readonly ISwitchesService _switchesService;
        private readonly IRoomsService _roomsService;

        public SwitchController(ISwitchesService moviesService, IRoomsService roomsServices)
        {
            _switchesService = moviesService;
            _roomsService = roomsServices;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var _switch = AutoMapper.Mapper.Map<List<SwitchResponse>>(_switchesService.GetAll());
            return Ok(_switch);
        }

        /// <summary>
        /// Get switch by id
        /// </summary>
        /// <param name="switchId">movie identifier</param>
        /// <returns>Switch if found</returns>
        [HttpGet("{movieId}")]
        [ExecutionTime]
        public IActionResult Get(int switchId)
        {
            Switch _switch = _switchesService.GetById(switchId);

            if (_switch == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<SwitchResponse>(_switch));
        }

        /// <summary>
        /// Add new switch to repositorium
        /// </summary>
        /// <param name="switch">new switch</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationAttribute]
        public IActionResult Post([FromBody]SwitchRequest _switch)
        {
            _switchesService.AddNewSwitch(AutoMapper.Mapper.Map<Switch>(_switch));

            return Ok();
        }

        /// <summary>
        /// Update switch in repositorium
        /// </summary>
        /// <param name="switch">updated switch</param>
        /// <returns></returns>
        [HttpPut("{switchId}")]
        public IActionResult Put(int switchId, [FromBody]SwitchRequest switchRequest)
        {
            var _switch = AutoMapper.Mapper.Map<Switch>(switchRequest);
            _switch.Id = switchId;
            if (_switchesService.UpdateSwitch(_switch))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete switch from repositorium
        /// </summary>
        /// <param name="switchId">switch identifier</param>
        /// <returns></returns>
        [HttpDelete("{switchId}")]
        public IActionResult Delete(int switchId)
        {
            _switchesService.Remove(switchId);
            return Ok();
        }
    }
}
