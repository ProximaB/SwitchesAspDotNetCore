using System;
using System.Data.SqlTypes;

namespace SwitchesAPI.Models
{
    public class RoomResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TypeSmallDateTimeSchemaImporterExtension CreateDate { get; set; }
    }
}
