﻿namespace SwitchesAPI.Models
{
    public class SwitchResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string State { get; set; }

        public string AddTime { get; set; }

        public int RoomId { get; set; }
    }
}
