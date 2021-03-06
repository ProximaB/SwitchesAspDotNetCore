﻿using System.ComponentModel.DataAnnotations;

namespace SwitchesAPI.Models
{
    public class SwitchRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int RoomId { get; set; }
    }
}
