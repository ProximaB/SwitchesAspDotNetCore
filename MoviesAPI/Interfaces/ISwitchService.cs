using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface ISwitchService
    {
        List<Switch> GetAll();

        Switch GetById(int id);

        void AddNewSwitch(Switch _switch);

        bool UpdateSwitch(Switch _switch);

        void Remove(int switchID);
    }
}
