using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IRoomService
    {
        List<Room> GetAll();

        Room GetById(int id);

        List<Room> GetByRoomId(int roomId);

        bool AddNewRoom(Room room);

        bool UpdateRoom(Room room);

        void Remove(int roomId);
    }
}
