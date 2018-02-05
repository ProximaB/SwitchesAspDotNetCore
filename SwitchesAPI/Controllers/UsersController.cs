using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Handlers.WebSocketsHandlers;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

       // private readonly SwitchChangedHandler _switchChangedHandler;

        private readonly NotificationsMessageHandler _notificationsMessageHandler;
        // Adding New User, New user first login set password
        // Adding Switches to users....
        // returning Get Switches depend on user login in

        public UsersController (IUsersService usersService, SwitchChangedHandler switchChangedHandler, NotificationsMessageHandler notificationsMessageHandler)
        {
            this.usersService = usersService;
            _notificationsMessageHandler = notificationsMessageHandler;
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll ()
        {
            return Ok(AutoMapper.Mapper.Map<List<UserResponse>>(usersService.GetAll()));
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="userId">Room id</param>
        /// <returns>Room if exist</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById (int userId)
        {
            User user = usersService.GetById(userId);

            if ( user == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// Get User's List of switches
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Room's switches if exist</returns>
        [HttpGet("{id}/Switches")]
        public IActionResult GetUserSwitches (int id)
        {
            var switches = usersService.GetUserSwitches(id);

            if ( switches == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<SwitchResponse>>(switches));
        }

        /// <summary>
        /// Get User's Switch
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="switchId">Switch Id</param>
        /// <returns>Room's switches if exist</returns>
        [HttpGet("{UserId}/Switches/{switchId}")]
        public IActionResult GetUserSwitch (int userId, int switchId)
        {
            var switches = usersService.GetUserSwitches(userId);
            var _switch = switches.FirstOrDefault(s => s.Id == switchId);

            if ( _switch == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<SwitchResponse>(_switch));
        }

        // <summary>
        /// Get User's rooms.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("{id}/Rooms")]
        public IActionResult GetUserRooms (int id)
        {
            var rooms = usersService.GetUserRooms(id);

            if ( rooms == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(rooms));
        }
        // <summary>
        /// Add exist switch to User.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="switchId">Switch Id</param>
        /// <response code="200">User added!</response>
        /// <returns></returns>
        [HttpGet("{userId}/AddSwitch/{switchId}")]
        public IActionResult AddSwitchToUser (int userId, int switchId)
        {
            if ( !usersService.AddSwitchToUser(switchId, userId) )
            {
                return BadRequest();
            }

            return (GetUserSwitch(userId, switchId));
        }

        [HttpGet("{userName}/GetUserCredentials")]
        public IActionResult GetUserCredentials (string userName)
        {
            return Ok(usersService.GetUserCredentials(userName));
        }

        // <summary>
        /// Add new User to repositorium.
        /// <summary></summary>
        /// <para name="user">user</para>
        /// <returns></returns>
        [HttpPost]
        [ModelValidation]
        public IActionResult Post ([FromBody] UserRequest user)
        {
            if ( !usersService.AddNewUser(Mapper.Map<User>(user)) )
            {
                return BadRequest();
            }

            int userId = usersService.LastUpdatedId ?? throw new NullReferenceException();
            var createdUser = usersService.GetById(userId);

            return Ok(Mapper.Map<UserResponse>(createdUser));

        }

        /// <summary>
        ///     Add new switch to repositorium
        /// </summary>
        /// <param name="userId">new switch</param>
        /// <param name="_switch">new switch</param>
        /// <returns></returns>
        [HttpPost("{userId}/Switches")]
        [ModelValidation]
        public IActionResult Post (int userId, [FromBody] SwitchRequest _switch)
        {
            if ( !usersService.AddNewSwitchToRepo(userId, Mapper.Map<Switch>(_switch)) )
            {
                return BadRequest();
            }

            int switchId = usersService.LastUpdatedId ?? throw new NullReferenceException();

            return GetUserSwitch(userId, switchId);
        }


        /// <summary>
        ///     Update User in repositorium
        /// </summary>
        /// <param name="userId">Updated userId</param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        public IActionResult Put (int userId, [FromBody] UserRequest _user)
        {
            User user = Mapper.Map<User>(_user);
            user.Id = userId;

            if ( !usersService.UpdateUser(user) )
            {
                return BadRequest();
            }

            // var sw = _switchesService.GetById(switchId);
            // return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            return GetUserById(userId);
        }

        /// <summary>
        /// Update User's Switch in repositorium
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="switchId">updated switchId</param>
        /// <param name="_switch">updated switch</param>
        /// <returns></returns>
        [HttpPut("{userId}/Switches/{switchId}")]
        public IActionResult Put (int userId, int switchId, [FromBody] RoomRequest _switch)
        {
            if ( !usersService.UpdateUserSwitch(userId, switchId, AutoMapper.Mapper.Map<Switch>(_switch)) )
            {
                return BadRequest();
            }

            //Room _room = roomsService.GetById(roomId);
            // return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(_room));   
            return GetUserSwitch(userId, switchId);
        }

        /// <summary>
        ///     Update state of switch by Id.
        /// </summary>
        /// <param name="userId">Updated switch Id</param>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="state">state of Switch [ON/OFF]</param>
        /// <returns></returns>
        [HttpPut("{userId}/Switches/{switchId}/{state}")] //TODO
        public async Task<IActionResult> PutAsync (int userId, int switchId, string state)
        {
            var switches = usersService.GetUserSwitches(userId);
            var _swth = switches.FirstOrDefault(s => s.Id == switchId);

            if ( _swth.State == state )
            {
                return GetUserSwitch(userId, switchId);
                // return NoContent();
            }

            _swth.State = state;
            if ( !usersService.UpdateUserSwitch(userId, switchId, _swth) )
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
            return GetUserSwitch(userId, switchId);
        }


        /// <summary>
        ///     Delete User from repositorium
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public IActionResult Delete (int userId)
        {
            if ( !usersService.DeleteUser(userId) )
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
