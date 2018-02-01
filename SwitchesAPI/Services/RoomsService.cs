using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Extensions;
using SwitchesAPI.Interfaces;

namespace SwitchesAPI.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly SwitchesContext context;

        public int? LastUpdatedId { get; set; }

        public RoomsService (SwitchesContext context)
        {
            this.context = context;
        }

        public List<Room> GetAll ()
        {
            return context.Rooms.ToList();
        }

        public Room GetById (int roomId)
        {
            //return context.Rooms.Find(id);
            return context.Rooms.Where(x => x.Id == roomId).FirstOrDefault();
        }

        public List<Switch> GetSwitchesByRoomId (int roomId)
        {
            var room = context.Rooms.Where(r => r.Id == roomId).SingleOrDefault();
            if ( room == null )
            {
                return null;
            }

            return room.Switches.ToList();
        }

        public bool AddNewRoom (Room room)
        {
            if ( room == null )
            {
                return false;
            }

            room.LastModiDateTime = DateTime.Now;

            try
            {
                context.Rooms.Add(room);
                context.SaveChanges();
                LastUpdatedId = room.Id;
            }
            catch ( DbUpdateException )
            {
                return false;
            }

            return true;
        }

        public bool UpdateRoom (int roomId, Room room)
        {
            Room foundRoom = GetById(roomId);

            if ( foundRoom == null )
            {
                return false;
            }

            foundRoom.LastModiDateTime = DateTime.Now;
            foundRoom.Name = room.Name;
            foundRoom.Description = room.Description;

            try
            {
                context.SaveChanges();
                LastUpdatedId = room.Id;
            }
            catch ( DbUpdateException )
            {
                return false;
            }
            return true;
        }



        public bool Delete (int roomId)
        {
            Room room = GetById(roomId);
            if ( room == null )
            {
                return false;
            }

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