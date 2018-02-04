using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;

namespace SwitchesAPI.Interfaces
{
    public interface IUsersService
    {
        List<User> GetAll ();
        User GetById (int id);
        List<Switch> GetUserSwitches(int id);
        List<dynamic> GetUserRooms(int id);
        (string PasswordSalt, string Password) GetUserCredentials ();
        bool AddNewUser (User user);
        bool UpdateUser (User user);
        bool DeleteUser (User user);
        void operations ();
    }
}
// Adding New User, New user first login set password
// Adding Switches to users....
// returning Get Switches depend on user login in