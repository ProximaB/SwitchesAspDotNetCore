using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchesAPI.Models
{
    public class UserRequest
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}
