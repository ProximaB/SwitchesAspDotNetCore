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
        int? LastUpdatedId { get; }
        List<User> GetAll ();
        User GetById (int userId);
        User GetByUserName (string userName);
        List<Switch> GetUserSwitches (string userName);
        List<Room> GetUserRooms (string userName);
        bool UpdateUserSwitch (string userName, int switchId, Switch _switch);
        bool AddSwitchToUser (int switchId, string userName);
        bool AddNewSwitchToRepo (string userName, Switch _switch);
        (string PasswordSalt, string Password) GetUserCredentials (string userName);
        bool AddNewUser (User user);
        bool UpdateUser (User user);
        bool DeleteUser (string userName);

    }
}
// Adding New User, New user first login set password
// Adding Switches to users....
// returning Get Switches depend on user login in