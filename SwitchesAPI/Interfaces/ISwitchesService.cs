using System.Collections.Generic;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Interfaces
{
    public interface ISwitchesService
    {
        List<Switch> GetAll();

        Switch GetById(int switchId);

        bool AddNewSwitch(Switch swth);

        bool UpdateSwitch(Switch swth);

        bool Delete(int switchId);

        int? LastUpdatedId { get; }
    }
}
