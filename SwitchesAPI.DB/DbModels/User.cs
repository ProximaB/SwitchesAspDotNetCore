using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitchesAPI.Models;

namespace SwitchesAPI.DB.DbModels
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, MaxLength(20)]
        public string UserName { get; set; }
        [Column("Password Salt")]
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        [Column("Create Date")]
        public DateTime CreateDate { get; set; }

        public ICollection<UserSwitch> UserSwitches { get; } = new List<UserSwitch>();
    }
}
