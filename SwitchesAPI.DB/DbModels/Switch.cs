using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchesAPI.DB.DbModels
{
    public class Switch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("Id")]
        public int Id { get; set; }

        //[Index(IsUnique = true), Column("UniqueString"), MaxLength(11)]
        //public string UniqueStr { get; set; }

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
    }
}
