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
        public string RoomId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column("Last modified DateTime",TypeName = "DateTime2")]
        public DateTime LastModiDateTime { get; set;  }

        public virtual ICollection<Switch> Switches { get; set; }
    }
}
