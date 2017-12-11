using System.Collections.Generic;

namespace SwitchesAPI.DB.DbModels
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short Description { get; set; }

        public virtual ICollection<Switch> Switches { get; set; }
    }
}
