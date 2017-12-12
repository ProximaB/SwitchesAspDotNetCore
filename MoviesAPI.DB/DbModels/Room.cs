using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwitchesAPI.DB.DbModels
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Switch> Switches { get; set; }
    }
}
