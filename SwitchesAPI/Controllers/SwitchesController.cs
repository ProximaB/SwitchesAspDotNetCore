using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Handlers.WebSocketsHandlers;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class SwitchesController : Controller, INotifyPropertyChanged
    {
        private readonly NotificationsMessageHandler _notificationsMessageHandler;
        private readonly SwitchChangedHandler _switchChangedHandler;

        private readonly ISwitchesService _switchesService;

        public SwitchesController (ISwitchesService switchesService, 
            NotificationsMessageHandler notificationsMessageHandler, SwitchChangedHandler switchChangedHandler)
        {
            _switchesService = switchesService;
            _notificationsMessageHandler = notificationsMessageHandler;
            _switchChangedHandler = switchChangedHandler;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Get all Switeches
        /// </summary>
        /// <returns>List of Switches</returns>
        [HttpGet]
        public IActionResult GetAllSwitches ()
        {
            var _switches = Mapper.Map<List<SwitchResponse>>(_switchesService.GetAll());
            return Ok(_switches);
        }

        /// <summary>
        ///     Get switch by id
        /// </summary>
        /// <param name="switchId">movie identifier</param>
        /// <returns>Switch if found</returns>
        [HttpGet("{switchId}")]
        [ExecutionTime]
        public IActionResult Get (int switchId)
        {
            Switch _switch = _switchesService.GetById(switchId);

            if ( _switch == null )
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SwitchResponse>(_switch));
        }

        /// <summary>
        ///     Add new switch to repositorium
        /// </summary>
        /// <param name="_switch">new switch</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValidation]
        public IActionResult Post ([FromBody] SwitchRequest _switch)
        {
            if ( !_switchesService.AddNewSwitch(Mapper.Map<Switch>(_switch)) )
            {
                return BadRequest();
            }

            int switchId = _switchesService.LastUpdatedId ?? throw new NullReferenceException();
            var _swth = _switchesService.GetById(switchId);

            _switchChangedHandler.SendActionToAll(switchId, SwitchChangedHandler.Action.POST, Mapper.Map<SwitchResponse>(_swth));
            return Ok(Mapper.Map<SwitchResponse>(_swth));
        }

        /// <summary>
        ///     Update switch in repositorium
        /// </summary>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="switchRequest">obj Switch</param>
        /// <returns></returns>
        [HttpPut("{switchId}")]
        public IActionResult Put (int switchId, [FromBody] SwitchRequest switchRequest)
        {
            Switch swth = Mapper.Map<Switch>(switchRequest);

            if ( !_switchesService.UpdateSwitch(switchId, swth) )
            {
                return BadRequest();
            }

            // var sw = _switchesService.GetById(switchId);
            // return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            return Get(switchId);
        }

        /// <summary>
        ///     Update state of switch by Id.
        /// </summary>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="state">state of Switch [ON/OFF]</param>
        /// <returns></returns>
        [HttpPut("{switchId}/{state}")]
        public async Task<IActionResult> PutAsync (int switchId, string state)
        {
            var _swth = _switchesService.GetById(switchId);

            if ( _swth.State == state )
            {
                return Get(switchId);
                // return NoContent();
            }

            _swth.State = state;
            if ( !_switchesService.UpdateSwitch(switchId, _swth) )
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
            _notificationsMessageHandler.SendMessageToAllAsync(message);
            return Get(switchId);
        }

        /// <summary>
        ///     Delete switch from repositorium
        /// </summary>
        /// <param name="switchId">switch identifier</param>
        /// <returns></returns>
        [HttpDelete("{switchId}")]
        public IActionResult Delete (int switchId)
        {
            if ( !_switchesService.Delete(switchId) )
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}