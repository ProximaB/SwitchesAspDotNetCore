using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Common;
using SwitchesAPI.DB;
using System.Text;

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

        public Room GetById(string id)
        {
            //return context.Rooms.Find(id);
            return context.Rooms.Where(x => x.RoomId == id).FirstOrDefault();
        }

        public List<Switch> GetSwitchesByRoomId(string roomId)
        {
            var room = context.Rooms.Where(r => r.RoomId == roomId).SingleOrDefault();
            if(room == null)
            {
                return null;
            }

            return room.Switches.ToList();
        }

        public bool AddNewRoom(Room room, out string Id)
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            Id = builder.ToString();


            if (room == null) return false;

            room.LastModiDateTime = DateTime.Now;
            room.RoomId = Id;
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

        public bool UpdateRoom(string roomId, Room room)
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

        public bool Delete(string roomId)
        {
            Room room = GetById(roomId);
            if (room == null)
            {
                return false;
            }

            //var switches = room.Switches;
            //foreach (var sw in switches)
            //{
            //    context.Switches.Remove(sw);
            //}

            context.Rooms.Remove(room);
            context.SaveChanges();

            return true;
        }
    }
}
