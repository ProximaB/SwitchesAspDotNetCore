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

        //protected override void Seed(MoviesAPI.DB.SwitchesContext context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data.

        //   // context.Movies.AddOrUpdate(m => m.Title, new DbModels.Movie()
        //   // {
        //   ////     Title = "Dodany przez Seed",
        //   //     Year = 2017
        //   // });
        //}
    }
}
