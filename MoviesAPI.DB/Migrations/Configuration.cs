using SwitchesAPI.DB.DbModels;

namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SwitchesAPI.DB.SwitchesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SwitchesAPI.DB.SwitchesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Rooms.AddOrUpdate(x => x.Id,
                new Room() { Id = 1, Name = "Bedroom", Description = "Bedroom...", CreateDate = DateTime.Now },

                new Room() { Id = 2, Name = "Kitchen", Description = "Kitchen...", CreateDate = DateTime.Now },

                new Room() { Id = 3, Name = "Living Room", Description = "Living Room...", CreateDate = DateTime.Now }
            );

            context.Switches.AddOrUpdate(x => x.Id,
                new Switch() { Id = 1, Name = "Main Light", Description = "Main light switch...", RoomId = 1, State = "OFF", CreateDate = DateTime.Now },
                new Switch() { Id = 1, Name = "Night lamp", Description = "Nigh lamp switch...", RoomId = 1, State = "OFF", CreateDate = DateTime.Now },

                new Switch() { Id = 1, Name = "Main Light", Description = "Main light switch...", RoomId = 2, State = "OFF", CreateDate = DateTime.Now },
                new Switch() { Id = 1, Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, State = "OFF", CreateDate = DateTime.Now },

                new Switch() { Id = 1, Name = "Main Light", Description = "Main light switch...", RoomId = 2, State = "OFF", CreateDate = DateTime.Now },
                new Switch() { Id = 1, Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, State = "OFF", CreateDate = DateTime.Now }
            );
        }
    }
}
