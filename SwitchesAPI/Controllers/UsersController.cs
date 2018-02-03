using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.Interfaces;

namespace SwitchesAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        // Adding New User, New user first login set password
        // Adding Switches to users....
        // returning Get Switches depend on user login in

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetUserSwitches ()
        {
            _usersService.GetUserSwitches(1);

            return Ok();
        }

    }
}
