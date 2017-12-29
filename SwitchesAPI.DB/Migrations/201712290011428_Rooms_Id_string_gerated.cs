namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rooms_Id_string_gerated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Switches", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Switches", new[] { "RoomId" });
            AddColumn("dbo.Switches", "Room_Id", c => c.Int());
            AlterColumn("dbo.Switches", "RoomId", c => c.String(nullable: false));
            CreateIndex("dbo.Switches", "Room_Id");
            AddForeignKey("dbo.Switches", "Room_Id", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Switches", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Switches", new[] { "Room_Id" });
            AlterColumn("dbo.Switches", "RoomId", c => c.Int(nullable: false));
            DropColumn("dbo.Switches", "Room_Id");
            CreateIndex("dbo.Switches", "RoomId");
            AddForeignKey("dbo.Switches", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
