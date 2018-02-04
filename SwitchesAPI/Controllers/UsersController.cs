using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
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

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
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

            if (user == null)
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

        [Route("operator")]
        [HttpGet]
        public IActionResult AddSwitchToUser()
        {
            usersService.operations();
            return Ok();
        }
    }
}
 