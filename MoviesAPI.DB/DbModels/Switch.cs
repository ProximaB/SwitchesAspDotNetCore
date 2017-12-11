using System.Collections.Generic;

namespace SwitchesAPI.DB.DbModels
{
  
    public class Switch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short Description { get; set; }

        public string State { get; set; }

        public string AddTime { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
