using System.Collections.Generic;
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
            var foundRoom = this.context.Rooms.SingleOrDefault(r => r.Id == room.Id);
            if(foundRoom == null)
            {
                return false;
            }

            context.Rooms.Add(room);
            return true;
        }

        public bool UpdateRoom(Room room)
        {
            Room foundRoom = GetById(room.Id);

            if (foundRoom == null)
            {
                return false;
            }

            foundRoom.Name = room.Name;
            foundRoom.Switches = room.Switches.ToList(); //?? cop list ref to ref
            context.SaveChanges();

            return true;
        }

        public void Remove(int roomId)
        {
            Room room = GetById(roomId);
            context.Rooms.Remove(room);
            context.SaveChanges();
        }
    }
}
