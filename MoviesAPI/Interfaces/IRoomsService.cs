using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface IRoomsService
    {
        List<Room> GetAll();

        Room GetById(int id);

        List<Switch> GetByRoomId(int roomId);

        bool AddNewRoom(Room room);

        bool UpdateRoom(Room room);

        bool Delete(int roomId);
    }
}
