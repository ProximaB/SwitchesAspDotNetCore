namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Generated_Key_Lint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Switches", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Switches", new[] { "RoomId" });
            DropPrimaryKey("dbo.Rooms");
            AddColumn("dbo.Rooms", "RoomId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Rooms", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Switches", "RoomId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Rooms", "RoomId");
            CreateIndex("dbo.Switches", "RoomId");
            AddForeignKey("dbo.Switches", "RoomId", "dbo.Rooms", "RoomId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Switches", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Switches", new[] { "RoomId" });
            DropPrimaryKey("dbo.Rooms");
            AlterColumn("dbo.Switches", "RoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rooms", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Rooms", "RoomId");
            AddPrimaryKey("dbo.Rooms", "Id");
            CreateIndex("dbo.Switches", "RoomId");
            AddForeignKey("dbo.Switches", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
