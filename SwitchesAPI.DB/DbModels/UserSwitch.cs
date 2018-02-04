using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.Models
{
    public class UserSwitch
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SwitchId { get; set; }
        public  Switch Switch { get; set; }
    }
}
