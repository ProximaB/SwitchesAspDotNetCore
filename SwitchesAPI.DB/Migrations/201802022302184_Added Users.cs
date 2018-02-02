namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        PasswordSalt = c.String(name: "Password Salt"),
                        Password = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserSwitches",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SwitchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.SwitchId })
                .ForeignKey("dbo.Users", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Switches", t => t.SwitchId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.SwitchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSwitches", "SwitchId", "dbo.Switches");
            DropForeignKey("dbo.UserSwitches", "Id", "dbo.Users");
            DropIndex("dbo.UserSwitches", new[] { "SwitchId" });
            DropIndex("dbo.UserSwitches", new[] { "Id" });
            DropTable("dbo.UserSwitches");
            DropTable("dbo.Users");
        }
    }
}
