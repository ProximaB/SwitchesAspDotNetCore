namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueString = c.String(maxLength: 11),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        LastmodifiedDateTime = c.DateTime(name: "Last modified DateTime", nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UniqueString, unique: true);
            
            CreateTable(
                "dbo.Switches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueString = c.String(maxLength: 11),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        State = c.String(nullable: false),
                        LastmodifiedDateTime = c.DateTime(name: "Last modified DateTime", nullable: false, precision: 7, storeType: "datetime2"),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.UniqueString, unique: true)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Switches", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Switches", new[] { "RoomId" });
            DropIndex("dbo.Switches", new[] { "UniqueString" });
            DropIndex("dbo.Rooms", new[] { "UniqueString" });
            DropTable("dbo.Switches");
            DropTable("dbo.Rooms");
        }
    }
}
