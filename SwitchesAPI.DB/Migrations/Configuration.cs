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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SwitchesAPI.DB.SwitchesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Rooms.AddOrUpdate(x => x.Id,
                new Room() { Id = 1, UniqueStr ="1", Name = "Bedroom", Description = "Bedroom...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 2, UniqueStr = "2", Name = "Kitchen", Description = "Kitchen...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 3, UniqueStr = "3", Name = "Living Room", Description = "Living Room...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 4, UniqueStr = "4", Name = "Bathroom", Description = "Bathroom..", LastModiDateTime = DateTime.Now }
            );

            context.Switches.AddOrUpdate(x => x.Id,
                new Switch() { Id = 1, Name = "Main Light", Description = "Main light switch...", RoomId = 1, UniqueStr = "1", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 2, Name = "Night lamp", Description = "Nigh lamp switch...", RoomId = 1, UniqueStr = "2", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 3, Name = "Main Light", Description = "Main light switch...", RoomId = 2, UniqueStr = "3", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 4, Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, UniqueStr = "4", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 5, Name = "Main Light", Description = "Main light switch...", RoomId = 3, UniqueStr = "5", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 6, Name = "Power outlet", Description = "Power outlet behind the tv...", RoomId = 4, UniqueStr = "6", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 7, Name = "Main Light", Description = "Main light switch...", RoomId = 5, UniqueStr = "7", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 8, Name = "Shower light", Description = "Shower ceiling light...", RoomId = 5, UniqueStr = "8", State = "OFF", LastModifieDateTime = DateTime.Now }
            );
        }
    }
}