using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchesAPI.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
