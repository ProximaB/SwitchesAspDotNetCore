using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Models;

namespace SwitchesAPI.DB.DbInitializer
{
    public static class DbInitializer
    {
        public static void Initialize (SwitchesContext context)
        {

            if ( context.Switches.Any() )
            {
                return;
            }

            context.Rooms.AddRange(
                new Room() { Name = "Bedroom", Description = "Bedroom...", LastModiDateTime = DateTime.Now },

                new Room() { Name = "Kitchen", Description = "Kitchen...", LastModiDateTime = DateTime.Now },

                new Room() { Name = "Living Room", Description = "Living Room...", LastModiDateTime = DateTime.Now },

                new Room() { Name = "Bathroom", Description = "Bathroom..", LastModiDateTime = DateTime.Now }
            );
            context.SaveChanges();

            context.Switches.AddRange(
                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 1, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Night lamp", Description = "Nigh lamp switch...", RoomId = 1, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 2, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 3, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Power outlet", Description = "Power outlet behind the tv...", RoomId = 3, State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Name = "Main Light", Description = "Main light switch...", RoomId = 4, State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Name = "Shower light", Description = "Shower ceiling light...", RoomId = 4, State = "OFF", LastModifieDateTime = DateTime.Now }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User() { Name = "User1", CreateDate = DateTime.Now },
                new User() { Name = "User2", CreateDate = DateTime.Now },
                new User() { Name = "User3", CreateDate = DateTime.Now },
                new User() { Name = "User4", CreateDate = DateTime.Now }
            );
            context.SaveChanges();

            context.UserSwitches.AddRange(
                new UserSwitch() { UserId = 1, SwitchId = 1 },
                new UserSwitch() { UserId = 2, SwitchId = 2 },
                new UserSwitch() { UserId = 3, SwitchId = 3 },
                new UserSwitch() { UserId = 4, SwitchId = 4 },
                new UserSwitch() { UserId = 1, SwitchId = 5 },
                new UserSwitch() { UserId = 2, SwitchId = 6 },
                new UserSwitch() { UserId = 3, SwitchId = 7 },
                new UserSwitch() { UserId = 4, SwitchId = 8 }
                );
            context.SaveChanges();
        }
    }
}
