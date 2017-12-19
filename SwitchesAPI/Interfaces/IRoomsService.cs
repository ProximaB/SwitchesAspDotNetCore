using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface IRoomsService
    {
        List<Room> GetAll();

        Room GetById(int id);

        List<Switch> GetSwitchesByRoomId(int roomId);

        bool AddNewRoom(Room room);

        bool UpdateRoom(int roomId, Room room);

        bool Delete(int roomId);
    }
}
