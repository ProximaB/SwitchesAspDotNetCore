namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rooms", name: "Id", newName: "RoomId");
            RenameColumn(table: "dbo.Switches", name: "Id", newName: "SwitchId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Switches", name: "SwitchId", newName: "Id");
            RenameColumn(table: "dbo.Rooms", name: "RoomId", newName: "Id");
        }
    }
}
