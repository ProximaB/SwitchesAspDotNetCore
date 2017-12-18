using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface ISwitchesService
    {
        List<Switch> GetAll();

        Switch GetById(int id);

        bool AddNewSwitch(Switch _switch);

        bool UpdateSwitch(Switch _switch);

        bool Delete(int switchID);
    }
}
