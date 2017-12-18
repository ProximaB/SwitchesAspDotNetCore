using System.ComponentModel.DataAnnotations;

namespace SwitchesAPI.Models
{
    public class RoomRequestPut
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, 22, ErrorMessage = "Incorrect Name! Must be at least one letter and not longer than 22 letters.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
