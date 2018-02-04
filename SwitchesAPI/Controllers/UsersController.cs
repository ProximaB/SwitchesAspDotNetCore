using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;

namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        // Adding New User, New user first login set password
        // Adding Switches to users....
        // returning Get Switches depend on user login in

        public UsersController (IUsersService usersService)
        {
            this.usersService = usersService;
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
        /// Get User by Id
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <returns>Room if exist</returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById (int id)
        {
            User user = usersService.GetById(id);

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
        [HttpGet]
        [Route("{id}/Switches")]
        public IActionResult GetUserSwitches (int id)
        {
            var switches = usersService.GetUserSwitches(id);

            if ( switches == null )
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<List<SwitchResponse>>(switches));
        }

        // <summary>
        /// Get User's rooms.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/Rooms")]
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
        /// Add switch to User.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="switchId">Switch Id</param>
        /// <returns></returns>
        [Route("{userId}/AddSwiitch/{switchId}")]
        [HttpGet]
        public IActionResult AddSwitchToUser(int userId, int switchId)
        {
            if (!usersService.AddSwitchToUser(userId, switchId))
            {
             return BadRequest();
            }

            return Ok();
        }

        [Route("{userName}/GetUserCredentials")]
        [HttpGet]
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
        public IActionResult Post([FromBody] UserRequest user)
        {
            if (!usersService.AddNewUser(Mapper.Map<User>(user)))
            {
                return BadRequest();
            }

            int userId = usersService.LastUpdatedId;
            var createdUser= usersService.GetById(userId);

            return Ok(Mapper.Map<UserResponse>(createdUser));

        }

        /// <summary>
        ///     Update User in repositorium
        /// </summary>
        /// <param name="userId">Updated userId</param>
        /// <param name="user">obj Switch</param>
        /// <returns></returns>
        [HttpPut("{switchId}")]
        public IActionResult Put (int userId, [FromBody] UserRequest _user)
        {
            User user = Mapper.Map<User>(_user);
            user.Id = userId;

            if ( !usersService.UpdateUser(user))
            {
                return BadRequest();
            }

            // var sw = _switchesService.GetById(switchId);
            // return Ok(AutoMapper.Mapper.Map<SwitchResponse>(sw));
            return GetById(userId);
        }

        /// <summary>
        ///     Delete User from repositorium
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{switchId}")]
        public IActionResult Delete (int UserId)
        {
            if ( !usersService.DeleteUser(UserId) )
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
