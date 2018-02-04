using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchesAPI.DB.DbModels
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("RoomId")]
        public int Id { get; set; }

        //[Index(IsUnique = true), Column("UniqueString"), MaxLength(11)]
        //public string UniqueStr { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column("Last modified DateTime", TypeName = "DateTime2")]
        public DateTime LastModiDateTime { get; set;  }

        public virtual List<Switch> Switches { get; set; }
    }
}
