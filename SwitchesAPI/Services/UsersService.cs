using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;

namespace SwitchesAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly SwitchesContext context;

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

            var userSwitches = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id).Select(us => us.Switch).ToList();

            return userSwitches;
        }

        public void operations ()
        {
            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();


            var switches = context.Switches.ToList();

            users.FirstOrDefault(u => u.Id == 3).UserSwitches.Add(new UserSwitch() { SwitchId = switches[0].Id }); //Działa

            context.SaveChanges();
            // context.Users.FirstOrDefault(u => u.Name == "Bartek").UserSwitches.Add(new UserSwitch() { SwitchId = switches[0].Id });
        }
        [Route("{id}/Rooms")]
        public List<dynamic> GetUserRooms (int id)
        {
            var users = context.Users
                .Include(e => e.UserSwitches)
                .ThenInclude(e => e.Switch)
                .ToList();

            var userSwitches = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id)
                .Select(us => us.Switch).GroupBy(sw => new { sw.Id, sw.RoomId }).ToList<dynamic>();

            var userSwitchesCount = users.FirstOrDefault(u => u.Id == id).UserSwitches.Where(e => e.UserId == id)
                .Select(us => us.Switch).GroupBy(sw => new {sw.Id, sw.RoomId}).Select(g => g.Count()).ToList();
            
           return userSwitches;
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