using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchesAPI.DB.DbModels
{
    public class Switch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("SwitchId")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required, RegularExpression(@"ON|OFF")]
        public string State { get; set; }

        [Column("Last modified DateTime", TypeName = "DateTime2")]
        public DateTime LastModifieDateTime { get; set; }

        [ForeignKey("Room"), Required]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
