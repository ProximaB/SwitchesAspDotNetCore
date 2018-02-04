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
        List<Switch> GetUserSwitches (int id);
        List<Room> GetUserRooms (int id);
        bool AddSwitchToUser (int switchId, int userId);
        (string PasswordSalt, string Password) GetUserCredentials (string userName);
        bool AddNewUser (User user);
        bool UpdateUser (User user);
        bool DeleteUser (int userId);

        int LastUpdatedId { get; }
    }
}
// Adding New User, New user first login set password
// Adding Switches to users....
// returning Get Switches depend on user login in