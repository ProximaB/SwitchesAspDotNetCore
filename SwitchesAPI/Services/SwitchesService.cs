using System;
using System.Collections.Generic;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.DB;
using SwitchesAPI.Extensions;

namespace SwitchesAPI.Services
{
    public class SwitchesService : ISwitchesService
    {
        private readonly SwitchesContext _context;

        public SwitchesService(SwitchesContext context)
        {
            _context = context;
        }

        public List<Switch> GetAll()
        {
            return _context.Switches.ToList();
        }

        public Switch GetById(int switchId)
        {
            return _context.Switches.Find(switchId);
        }

        public bool AddNewSwitch(Switch _switch, out string uniqueStr)
        {
            uniqueStr = String.Empty.IdBuilder(11);

            if (_switch == null) return false;

            _switch.UniqueStr = uniqueStr;
            _switch.LastModifieDateTime = DateTime.Now;
            _context.Switches.Add(_switch);

            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {

                return false;
            }

            return true;
        }

        public bool UpdateSwitch(Switch _switch)
        {
            
            Switch foundSwitch = _context.Switches.Find(_switch.Id);

            if (foundSwitch == null)
            {
                return false;
            }
            foundSwitch.LastModifieDateTime = DateTime.Now;
            foundSwitch.Name = _switch.Name;
            foundSwitch.Description = _switch.Description;
            foundSwitch.RoomId = _switch.RoomId;
            foundSwitch.State = _switch.State;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            

            return true;
        }

        public bool Delete(int switchId)
        {
            Switch foundSwitch = _context.Switches.Find(switchId);

            if (foundSwitch == null)
                return false;

            _context.Switches.Remove(foundSwitch);

            try
            {
                
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return false;
            }

            return true;
        }

        public Switch GetByUniqueStr(string uniqueStr)
        {
            return _context.Switches.Where(s => s.UniqueStr == uniqueStr).FirstOrDefault();
        }
    }
}
