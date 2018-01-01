using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface ISwitchesService
    {
        List<Switch> GetAll();

        Switch GetById(int switchId);

        bool AddNewSwitch(Switch _switch, out string uniqueStr);

        bool UpdateSwitch(Switch _switch);

        bool Delete(int switchId);
        Switch GetByUniqueStr(string uniqueStr);
    }
}
