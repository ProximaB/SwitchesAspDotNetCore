using System.Collections.Generic;
using System.Linq;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;

namespace SwitchesAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly SwitchesContext context;
        public List<User> GetAll ()
        {
            return context.Users.ToList();
        }

        public User GetById (int id)
        {
            return context.Users.Find(id);
        }

        public List<Switch> GetUserSwitches (int id)
        {
            var userSwitches = from _switch in context.Switches
                               from _users in context.Users
                               select new
                               {
                                   UserName = _users.Name,
                                   SwitchName = _switch.Name,
                               };
            throw new System.NotImplementedException();
        }

        public List<Room> GetUserRooms (int id)
        {
            throw new System.NotImplementedException();
        }

        public (string PasswordSalt, string Password) GetUserCredentials ()
        {
            throw new System.NotImplementedException();
        }

        public bool AddNewUser (User user)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUser (User user)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteUser (User user)
        {
            throw new System.NotImplementedException();
        }
    }
}