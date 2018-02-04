using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.DB.DbInitializer
{
    public static class DbInitializer
    {
        public static void Initialize (SwitchesContext context)
        {
            // Look for any students.
            if ( context.Switches.Any() )
            {
                return;   
            }


            context.Rooms.AddRange(
                new Room() { Name = "Bedroom", Description = "Bedroom...", LastModiDateTime = DateTime.Now },

                new Room() { Name = "Kitchen", Description = "Kitchen...", LastModiDateTime = DateTime.Now },

                new Room() { Name = "Living Room", Description = "Living Room...", LastModiDateTime = DateTime.Now },

                new Room() {  Name = "Bathroom", Description = "Bathroom..", LastModiDateTime = DateTime.Now }
            );
            context.SaveChanges();

            context.Switches.AddRange(
                new Switch() {  Name = "Main Light", Description = "Main light switch...", RoomId = 1, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() {  Name = "Night lamp", Description = "Nigh lamp switch...", RoomId = 1, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() {  Name = "Main Light", Description = "Main light switch...", RoomId = 2, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() {  Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 3, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Power outlet", Description = "Power outlet behind the tv...", RoomId = 3, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 4, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Shower light", Description = "Shower ceiling light...", RoomId = 4, State = "OFF", LastModifieDateTime = DateTime.Now }
            );
            context.SaveChanges();

           // context.Users.AddRange(
            //    new User() { Name = "User1", Password = IdBuilder(), PasswordSalt = IdBuilder(), CreateDate = DateTime.Now });
           // context.SaveChanges();
        }

        static string IdBuilder ()
        {
            StringBuilder IdBuilder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char) e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char) e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => IdBuilder.Append(e));

            return IdBuilder.ToString();
        }
    }
}
