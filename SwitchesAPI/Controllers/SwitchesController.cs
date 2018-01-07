using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Handlers.WebSocketsHandlers;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class SwitchesController : Controller
    {
        private readonly ISwitchesService _switchesService;
        private readonly IRoomsService _roomsService;
        private readonly NotificationsMessageHandler _notificationsMessageHandler;

        public SwitchesController(ISwitchesService switchesService, IRoomsService roomsServices, NotificationsMessageHandler notificationsMessageHandler)
        {
            _switchesService = switchesService;
            _roomsService = roomsServices;
            _notificationsMessageHandler = notificationsMessageHandler;
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
        [ModelValidation]
        public IActionResult Post([FromBody]SwitchRequest _switch)
        {
            if (!_switchesService.AddNewSwitch(AutoMapper.Mapper.Map<Switch>(_switch), out string uniqueStr))
            {
                return BadRequest();
            }

            var sw =_switchesService.GetByUniqueStr(uniqueStr);
            return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));

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

            if (!_switchesService.UpdateSwitch(_switch))
            {
                return BadRequest();
            }

            // var sw = _switchesService.GetById(switchId);
            // return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            return Get(switchId);

        }

        /// <summary>
        /// Update state of switch by Id.
        /// </summary>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="state">state of Switch [ON/OFF]</param>
        /// <returns></returns>
        [HttpPut("{switchId}/{state}")]
        public async Task<IActionResult> PutAsync(int switchId, string state)
        {
            var _switch = _switchesService.GetById(switchId);

            if (_switch.State == state)
            {
                return Get(switchId);
               // return NoContent();
            }

            _switch.State = state;
            if (!_switchesService.UpdateSwitch(_switch))
            {
                return BadRequest();
            }

            //if (!_switchesService.UpdateSwitch(_switch))
            //{
            //    return BadRequest("State can only be \"ON\" or \"OFF\")");
            //}

            //var sw = _switchesService.GetById(switchId);
            //return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            var message = $"{switchId}:{state}";
            await _notificationsMessageHandler.SendMessageToAllAsync(message);
            return Get(switchId);
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
