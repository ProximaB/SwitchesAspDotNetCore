using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface IRoomsService
    {
        List<Room> GetAll();

        Room GetById(string id);

        List<Switch> GetSwitchesByRoomId(string roomId);

        bool AddNewRoom(Room room, out string Id);

        bool UpdateRoom(string roomId, Room room);

        bool Delete(string roomId);
    }
}
