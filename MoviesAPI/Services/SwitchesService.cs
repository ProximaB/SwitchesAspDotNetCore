﻿using System;
using System.Collections.Generic;
using System.Linq;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Interfaces;
using SwitchesAPI.DB;

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

        public Switch GetById(int id)
        {
            return _context.Switches.Find(id);
        }

        public bool AddNewSwitch(Switch _switch)
        {
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
