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
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll ()
        {
            return Ok(AutoMapper.Mapper.Map<List<UserResponse>>(usersService.GetAll()));
        }

        /// <summary>
        /// Get User by name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Room if exist</returns>
        [HttpGet("{userName}")]
        public IActionResult GetUserById (string userName)
        {
            User user = usersService.GetByUserName(userName);

            if ( user == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// Get User's List of switches
        /// </summary>
        /// <param name="userName">user Id</param>
        /// <returns>Room's switches if exist</returns>
        [HttpGet("{userName}/Switches")]
        public IActionResult GetUserSwitches (string userName)
        {
            var switches = usersService.GetUserSwitches(userName);

            if ( switches == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<SwitchResponse>>(switches));
        }

        /// <summary>
        /// Get User's Switch
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="switchId">Switch Id</param>
        /// <returns>Room's switches if exist</returns>
        [HttpGet("{userName}/Switches/{switchId}")]
        public IActionResult GetUserSwitch (string userName, int switchId)
        {
            var switches = usersService.GetUserSwitches(userName);
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
        /// <param name="userName">Username</param>
        /// <returns></returns>
        [HttpGet("{id}/Rooms")]
        public IActionResult GetUserRooms (string userName)
        {
            var rooms = usersService.GetUserRooms(userName);

            if ( rooms == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(rooms));
        }
        // <summary>
        /// Add exist switch to User.
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="switchId">Switch Id</param>
        /// <response code="200">User added!</response>
        /// <returns></returns>
        [HttpGet("{userName}/AddSwitch/{switchId}")]
        public IActionResult AddSwitchToUser (string userName, int switchId)
        {
            if ( !usersService.AddSwitchToUser(switchId, userName) )
            {
                return BadRequest();
            }

            return (GetUserSwitch(userName, switchId));
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

            int userId= usersService.LastUpdatedId ?? throw new NullReferenceException();
            var createdUser = usersService.GetById(userId);

            return Ok(Mapper.Map<UserResponse>(createdUser));

        }

        /// <summary>
        ///     Add new switch to repositorium
        /// </summary>
        /// <param name="userName">new switch</param>
        /// <param name="_switch">new switch</param>
        /// <returns></returns>
        [HttpPost("{userName}/Switches")]
        [ModelValidation]
        public IActionResult Post (string userName, [FromBody] SwitchRequest _switch)
        {
            if ( !usersService.AddNewSwitchToRepo(userName, Mapper.Map<Switch>(_switch)) )
            {
                return BadRequest();
            }

            int switchId = usersService.LastUpdatedId ?? throw new NullReferenceException();

            return GetUserSwitch(userName, switchId);
        }


        /// <summary>
        ///     Update User in repositorium
        /// </summary>
        /// <param name="userName">Updated userName</param>
        /// <param name="_user">Updated userName</param>
        /// <returns></returns>
        [HttpPut("{userName}")]
        public IActionResult Put (string userName, [FromBody] UserRequestPut _user)
        {
            User user = Mapper.Map<User>(_user);
            user.UserName = userName;

            if ( !usersService.UpdateUser(user) )
            {
                return BadRequest();
            }

            // var sw = _switchesService.GetById(switchId);
            // return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            return GetUserById(userName);
        }

        /// <summary>
        /// Update User's Switch in repositorium
        /// </summary>
        /// <param name="userName">user Id</param>
        /// <param name="switchId">updated switchId</param>
        /// <param name="_switch">updated switch</param>
        /// <returns></returns>
        [HttpPut("{userName}/Switches/{switchId}")]
        public async Task<IActionResult> PutAsync (string userName, int switchId, [FromBody] SwitchRequest _switch)
        {
            if ( !usersService.UpdateUserSwitch(userName, switchId, AutoMapper.Mapper.Map<Switch>(_switch)) )
            {
                return BadRequest();
            }

            //Room _room = roomsService.GetById(roomId);
            // return Ok(AutoMapper.Mapper.Map<List<RoomResponse>>(_room));   
            var message = $"{switchId}:{_switch.State}";
            _notificationsMessageHandler.SendMessageToAllAsync(message);
            return GetUserSwitch(userName, switchId);
        }

        /// <summary>
        ///     Update state of switch by Id.
        /// </summary>
        /// <param name="userName">Updated switch Id</param>
        /// <param name="switchId">Updated switch Id</param>
        /// <param name="state">state of Switch [ON/OFF]</param>
        /// <returns></returns>
        [HttpPut("{userName}/Switches/{switchId}/{state}")] //TODO
        public async Task<IActionResult> PutAsync (string userName, int switchId, string state)
        {
            var switches = usersService.GetUserSwitches(userName);
            var _swth = switches.FirstOrDefault(s => s.Id == switchId);

            if ( _swth.State == state )
            {
                return GetUserSwitch(userName, switchId);
                // return NoContent();
            }

            _swth.State = state;
            if ( !usersService.UpdateUserSwitch(userName, switchId, _swth) )
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
            return GetUserSwitch(userName, switchId);
        }


        /// <summary>
        ///     Delete User from repositorium
        /// </summary>
        /// <param name="userName">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{userName}")]
        public IActionResult Delete (string userName)
        {
            if ( !usersService.DeleteUser(userName) )
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
