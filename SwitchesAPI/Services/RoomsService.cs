using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Common;
using SwitchesAPI.DB;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using SwitchesAPI.Extensions;

namespace SwitchesAPI.Services
{
    public class RoomsService : IRoomsService
    {        
        private readonly SwitchesContext context;

        public RoomsService(SwitchesContext context)
        {   
            this.context = context;
        }

        public List<Room> GetAll()
        {
            return this.context.Rooms.ToList();
        }

        public Room GetById(int? roomId)
        {
            //return context.Rooms.Find(id);
            return context.Rooms.Where(x => x.Id == roomId).FirstOrDefault();
        }

        public List<Switch> GetSwitchesByRoomId(int roomId)
        {
            var room = context.Rooms.Where(r => r.Id == roomId).SingleOrDefault();
            if(room == null)
            {
                return null;
            }

            return room.Switches.ToList();
        }

        public bool AddNewRoom(Room room, out int? id)
        {
            id = null;

            if (room == null) return false;

            room.UniqueStr = String.Empty.IdBuilder(11);
            room.LastModiDateTime = DateTime.Now;
 
            try
            {
                context.Rooms.Add(room);
                context.SaveChanges();
                id = room.Id;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRoom(int roomId, Room room)
        {
            Room foundRoom = GetById(roomId);

            if (foundRoom == null)
            {
                return false;
            }

            foundRoom.LastModiDateTime = DateTime.Now;
            foundRoom.Name = room.Name;
            foundRoom.Description = room.Description;

            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int roomId)
        {
            Room room = GetById(roomId);
            if (room == null)
            {
                return false;
            }

            try
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return false;
            }

            return true;
        }

        public Room GetByUniqueStr(string uniqueStr)
        {
            return context.Rooms.Where(s => s.UniqueStr == uniqueStr).FirstOrDefault();
        }
    }
}
