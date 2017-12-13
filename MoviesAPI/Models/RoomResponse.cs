using System;

namespace SwitchesAPI.Models
{
    public class RoomResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime LastModiDateTime { get; set; }
    }
}
