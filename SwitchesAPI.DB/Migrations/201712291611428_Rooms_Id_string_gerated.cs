namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rooms_Id_string_gerated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Room_Id", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "RoomId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RoomId", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "Room_Id");
        }
    }
}
