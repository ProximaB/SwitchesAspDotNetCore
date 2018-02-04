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
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
