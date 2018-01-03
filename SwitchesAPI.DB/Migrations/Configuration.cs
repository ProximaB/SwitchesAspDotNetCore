namespace SwitchesAPI.DB.Migrations
{
    using SwitchesAPI.DB.DbModels;
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
                new Room() { Id = 1, UniqueStr = "tPgYqD0yfks", Name = "Bedroom", Description = "Bedroom...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 2, UniqueStr = "FKqiEIrUfHk", Name = "Kitchen", Description = "Kitchen...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 3, UniqueStr = "ysqYBHART1d", Name = "Living Room", Description = "Living Room...", LastModiDateTime = DateTime.Now },

                new Room() { Id = 4, UniqueStr = "NmdsA1i5VTx", Name = "Bathroom", Description = "Bathroom..", LastModiDateTime = DateTime.Now }
            );

            context.Switches.AddOrUpdate(x => x.Id,
                new Switch() { Id = 1, Name = "Main Light", Description = "Main light switch...", RoomId = 1, UniqueStr = "gPUV6xWvMZq", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 2, Name = "Night lamp", Description = "Nigh lamp switch...", RoomId = 1, UniqueStr = "oMENaCtK3be", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 3, Name = "Main Light", Description = "Main light switch...", RoomId = 2, UniqueStr = "h9bB15DGCXU", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 4, Name = "Led strap", Description = "Led strap above cabinet...", RoomId = 2, UniqueStr = "IvjVmOGREC5", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 5, Name = "Main Light", Description = "Main light switch...", RoomId = 3, UniqueStr = "nM5vF1CgcfP", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 6, Name = "Power outlet", Description = "Power outlet behind the tv...", RoomId = 4, UniqueStr = "dmPb0wJz8Xh", State = "OFF", LastModifieDateTime = DateTime.Now },

                new Switch() { Id = 7, Name = "Main Light", Description = "Main light switch...", RoomId = 5, UniqueStr = "TPzQlYaVomb", State = "OFF", LastModifieDateTime = DateTime.Now },
                new Switch() { Id = 8, Name = "Shower light", Description = "Shower ceiling light...", RoomId = 5, UniqueStr = "m7dkvpnP1he", State = "OFF", LastModifieDateTime = DateTime.Now }
            );
        }
    }
}
