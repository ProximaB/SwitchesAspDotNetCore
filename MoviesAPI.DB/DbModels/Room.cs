using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchesAPI.DB.DbModels
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "datetime2 ")]
        public DateTime CreateDate { get; set;  }

        public virtual ICollection<Switch> Switches { get; set; }
    }
}
