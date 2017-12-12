using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System.Collections.Generic;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class SwitchController : Controller
    {
        private readonly ISwitchesService _switchesService;
        private readonly IRoomsService _roomsService;

        public SwitchController(ISwitchesService switchesService, IRoomsService roomsServices)
        {
            _switchesService = switchesService;
            _roomsService = roomsServices;
        }

        /// <summary>
        /// Get all Switeches
        /// </summary>
        /// <returns>List of Switches</returns>
        [HttpGet]
        public IActionResult GetAllSwitches()
        {
            var _switches = AutoMapper.Mapper.Map<List<SwitchResponse>>(_switchesService.GetAll());
            return Ok(_switches);
        }

        /// <summary>
        /// Get switch by id
        /// </summary>
        /// <param name="switchId">movie identifier</param>
        /// <returns>Switch if found</returns>
        [HttpGet("{switchId}")]
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
        /// <param name="_switch">new switch</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidationAttribute]
        public IActionResult Post([FromBody]SwitchRequest _switch)
        {
            if (!_switchesService.AddNewSwitch(AutoMapper.Mapper.Map<Switch>(_switch)))
            {
                return BadRequest();
            }

            return Ok();

        }

        /// <summary>
        /// Update switch in repositorium
        /// </summary>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="switchRequest">obj Switch</param>
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
            if (!_switchesService.Delete(switchId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
