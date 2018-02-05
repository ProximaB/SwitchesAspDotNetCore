using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Extensions;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using SwitchesAPI.Handlers;

namespace SwitchesAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly SwitchesContext context;

        public int LastUpdatedId { get; private set; }

        public UsersService (SwitchesContext _context) => context = _context;

        public List<User> GetAll ()
        {
            return context.Users.ToList();
        }

        public User GetById (int id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<Switch> GetUserSwitches (int id)
        {
            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();

            //  var userSwitches = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id).Select(us => us.Switch).ToList();
            var userSwitches = context.UserSwitches.Include(u => u.Switch).Where(u => u.UserId == id)
                .Select(sw => sw.Switch).ToList();
            return userSwitches;
        }
       
        [Route("{id}/Rooms")]
        public List<Room> GetUserRooms (int id)
        {
            var rooms = new List<Room>();

            //var userSwitches = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id)
            //    .Select(us => us.Switch).GroupBy(sw => sw.RoomId).Select(sw => sw.ToList()).ToList();

            var userSwitches = context.UserSwitches.Include(u => u.Switch).Where(u => u.UserId == id)
                .Select(sw => sw.Switch);

            var userRoomsIds = userSwitches.GroupBy(sw => sw.RoomId).Select(g => g.Key).ToList();

            foreach (var roomId in userRoomsIds )
            {
                rooms.Add(context.Rooms.Find(roomId));
            }

            //var userSwitchesCount = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id)
            //    .Select(us => us.Switch).GroupBy(sw => new { sw.Id, sw.RoomId }).Select(g => g.Count()).ToList();
       
            return rooms;
        }

        public bool AddSwitchToUser(int switchId, int userId)
        {
            var _switch = context.Switches.Find(switchId);
            var user = context.Users.Find(userId);

            if ( _switch == null || user == null) return false;

            if (context.UserSwitches.FirstOrDefault(u => u.SwitchId == _switch.Id && u.UserId == user.Id) != null)
            {
                return true;
            }

            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();           

            try
            {
                users.FirstOrDefault(u => u.Id == user.Id)?.UserSwitches.Add(new UserSwitch() { SwitchId = _switch.Id });
                context.SaveChanges();
            }
            catch ( InvalidOperationException )
            {
                return false;
            }

            return true;

        }

        public (string PasswordSalt, string Password) GetUserCredentials (string userName)
        {
            var password = context.Users.FirstOrDefault(u => u.Name == userName)?.Password ?? "NULL";
            var passwordSalt = context.Users.FirstOrDefault(u => u.Name == userName)?.PasswordSalt ?? "NULL";

            return (passwordSalt, password);
        }

        public bool AddNewUser (User user)
        {

            if ( user == null )
            {
                return false;
            }

            user.CreateDate = DateTime.Now;
            user.PasswordSalt = String.Empty.GetSalt(11);
           
            user.Password = AuthenticationHashHandler.GenerateSaltedHash(user.Password, user.PasswordSalt);
 

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                LastUpdatedId = user.Id;
            }
            catch ( DbUpdateException )
            {
                return false;
            }

            return true;
        }

        public bool UpdateUser (User user)
        {
            User foundUser = GetById(user.Id);

            if ( foundUser == null )
            {
                return false;
            }

            foundUser.CreateDate = DateTime.Now;
            foundUser.Name = user.Name;

            foundUser.PasswordSalt = String.Empty.GetSalt(11);
            foundUser.Password = AuthenticationHashHandler.GenerateSaltedHash(user.Password, foundUser.PasswordSalt);

            try
            {
                context.SaveChanges();
                LastUpdatedId = user.Id;
            }
            catch ( DbUpdateException )
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser (int userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return true;
            }

            context.Users.Remove(user);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }
    }
}