namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_migrations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rooms", name: "Unique_String", newName: "Unique String");
            RenameColumn(table: "dbo.Switches", name: "Unique_String", newName: "Unique String");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Switches", name: "Unique String", newName: "Unique_String");
            RenameColumn(table: "dbo.Rooms", name: "Unique String", newName: "Unique_String");
        }
    }
}
