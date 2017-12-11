using System.Collections.Generic;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.DB;

namespace MoviesAPI.Services
{
    public class SwitchService : ISwitchService
    {
        private readonly SwitchesContext _context;

        public SwitchService(SwitchesContext context)
        {
            _context = context;
        }

        public List<Switch> GetAll()
        {
            return _context.Switches.ToList();
        }

        public Switch GetById(int id)
        {
            return _context.Switches.Find(id);
        }

        public void AddNewSwitch(Switch _switch)
        {
            _context.Switches.Add(_switch);
            _context.SaveChanges();
        }

        public bool UpdateSwitch(Switch _switch)
        {
            Switch foundSwitch = _context.Switches.Find(_switch.Id);

            if (foundSwitch == null)
            {
                return false;
            }

            foundSwitch.Name = _switch.Name;
            foundSwitch.RoomId = _switch.RoomId;
            foundSwitch.State = _switch.State;
            _context.SaveChanges();

            return true;
        }

        public void Remove(int switchId)
        {
            Switch foundSwitch = _context.Switches.Find(switchId);
            _context.Switches.Remove(foundSwitch);
            _context.SaveChanges();
        }
    }
}
