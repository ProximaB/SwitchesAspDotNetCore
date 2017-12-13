using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Common;
using SwitchesAPI.DB;

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

        public Room GetById(int id)
        {
            return context.Rooms.Find(id);
        }

        public List<Switch> GetByRoomId(int roomId)
        {
            var room = context.Rooms.Where(r => r.Id == roomId).SingleOrDefault();
            if(room == null)
            {
                return null;
            }

            return room.Switches.ToList();
        }

        public bool AddNewRoom(Room room)
        {
            if (room == null) return false;

            room.LastModiDateTime = DateTime.Now;
            context.Rooms.Add(room);
            context.SaveChanges();
        
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

        public bool UpdateRoom(Room room)
        {
            Room foundRoom = GetById(room.Id);

            if (foundRoom == null)
            {
                return false;
            }
            foundRoom.LastModiDateTime = DateTime.Now;
            foundRoom.Name = room.Name;
            foundRoom.Switches = room.Switches.ToList();
            context.SaveChanges();

            return true;
        }

        public bool Delete(int roomId)
        {
            Room room = GetById(roomId);
            if (room == null)
            {
                return false;
            }

            var switches = room.Switches;
            foreach (var sw in switches)
            {
                context.Switches.Remove(sw);
            }

            context.Rooms.Remove(room);
            context.SaveChanges();

            return true;
        }
    }
}
