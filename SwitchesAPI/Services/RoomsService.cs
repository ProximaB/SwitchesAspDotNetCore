using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Common;
using SwitchesAPI.DB;
using System.Text;
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

        public Room GetById(int roomId)
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

        public bool AddNewRoom(Room room, out string uniqueStr)
        {

            uniqueStr = string.Empty.IdBuilder(11);

            if (room == null) return false;

            room.LastModiDateTime = DateTime.Now;
            room.UniqueStr = uniqueStr;
            context.Rooms.Add(room);
        
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
