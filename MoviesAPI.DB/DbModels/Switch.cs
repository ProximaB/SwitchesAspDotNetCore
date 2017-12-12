using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchesAPI.DB.DbModels
{
  
    public class Switch
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required, RegularExpression(@"ON|OFF")]
        public string State { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
