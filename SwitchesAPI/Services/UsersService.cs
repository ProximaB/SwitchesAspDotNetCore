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

        public int? LastUpdatedId { get; private set; }

        public UsersService (SwitchesContext _context) => context = _context;

        public List<User> GetAll ()
        {
            return context.Users.ToList();
        }

        public User GetById (int userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetByUserName (string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public List<Switch> GetUserSwitches (string userName)
        {
            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();

            //  var userSwitches = users.FirstOrDefault(u => u.UserName == userName).UserSwitches.Where(e => e.UserName == userName).Select(us => us.Switch).ToList();
            var userSwitches = context.UserSwitches.Include(u => u.Switch).Where(u => u.UserName == userName)
                .Select(sw => sw.Switch).ToList();
            return userSwitches;
        }

        public List<Room> GetUserRooms (string userName)
        {
            var rooms = new List<Room>();

            //var userSwitches = users.FirstOrDefault(u => u.UserName == userName).UserSwitches.Where(e => e.UserName == userName)
            //    .Select(us => us.Switch).GroupBy(sw => sw.RoomId).Select(sw => sw.ToList()).ToList();

            var userSwitches = context.UserSwitches.Include(u => u.Switch).Include(u => u.Switch).ToList();

            var userRoomsIds = userSwitches.Where(u => u.UserName == userName).Select(sw => sw.Switch)
                .GroupBy(sw => sw.RoomId).Select(g => g.Key);

            foreach ( var roomId in userRoomsIds )
            {
                rooms.Add(context.Rooms.Find(roomId));
            }

            //var userSwitchesCount = users.FirstOrDefault(u => u.UserName == userName).UserSwitches.Where(e => e.UserName == userName)
            //    .Select(us => us.Switch).GroupBy(sw => new { sw.UserName, sw.RoomId }).Select(g => g.Count()).ToList();

            return rooms;
        }

        public bool UpdateUserSwitch (string userName, int switchId, Switch _switch)
        {
            var userSwitch = context.UserSwitches.Include(u => u.User).Include(s => s.Switch)
                .FirstOrDefault(us => us.UserName == userName && us.SwitchId == switchId);


            Switch foundSwitch = userSwitch.Switch;

            if ( foundSwitch == null )
            {
                return false;
            }

            foundSwitch.LastModifieDateTime = DateTime.Now;
            foundSwitch.Name = _switch.Name;
            foundSwitch.Description = _switch.Description;
            foundSwitch.RoomId = _switch.RoomId;
            foundSwitch.State = _switch.State;

            try
            {
                context.SaveChanges();
            }
            catch ( Exception )
            {
                return false;
            }


            return true;
        }


        public bool AddSwitchToUser (int switchId, string userName)
        {
            var _switch = context.Switches.Find(switchId);
            var user = context.Users.Find(userName);

            if ( _switch == null || user == null ) return false;

            if ( context.UserSwitches.FirstOrDefault(u => u.SwitchId == _switch.Id && u.UserName == user.UserName) != null )
            {
                return true;
            }

            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();

            try
            {
                users.FirstOrDefault(u => u.UserName == user.UserName)?.UserSwitches.Add(new UserSwitch() { SwitchId = _switch.Id });
                context.SaveChanges();
            }
            catch ( InvalidOperationException )
            {
                return false;
            }

            return true;

        }

        public bool AddNewSwitchToRepo (string userName, Switch _switch)
        {

            if ( _switch == null )
            {
                return false;
            }

            _switch.LastModifieDateTime = DateTime.Now;

            try
            {
                context.Switches.Add(_switch);
                context.SaveChanges();
                LastUpdatedId = _switch.Id;
            }
            catch ( DbUpdateException )
            {

                return false;
            }

            return AddSwitchToUser(_switch.Id, userName);
        }

        public (string PasswordSalt, string Password) GetUserCredentials (string userName)
        {
            var password = context.Users.FirstOrDefault(u => u.UserName == userName)?.Password ?? "NULL";
            var passwordSalt = context.Users.FirstOrDefault(u => u.UserName == userName)?.PasswordSalt ?? "NULL";

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
            User foundUser = GetByUserName(user.UserName);

            if ( foundUser == null )
            {
                return false;
            }


            foundUser.CreateDate = DateTime.Now;
            //foundUser.UserName = user.UserName;

            foundUser.PasswordSalt = String.Empty.GetSalt(11);
            foundUser.Password = AuthenticationHashHandler.GenerateSaltedHash(user.Password, foundUser.PasswordSalt);

            try
            {
                context.SaveChanges();
                LastUpdatedId = foundUser.Id;
            }
            catch ( DbUpdateException )
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser (string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);

            if ( user == null )
            {
                return true;
            }

            context.Users.Remove(user);

            try
            {
                context.SaveChanges();
            }
            catch ( DbUpdateException )
            {
                return false;
            }

            return true;
        }
    }
}