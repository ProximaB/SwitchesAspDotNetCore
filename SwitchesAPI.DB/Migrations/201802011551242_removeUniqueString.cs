namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUniqueString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Rooms", new[] { "UniqueString" });
            DropIndex("dbo.Switches", new[] { "UniqueString" });
            DropColumn("dbo.Rooms", "UniqueString");
            DropColumn("dbo.Switches", "UniqueString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Switches", "UniqueString", c => c.String(maxLength: 11));
            AddColumn("dbo.Rooms", "UniqueString", c => c.String(maxLength: 11));
            CreateIndex("dbo.Switches", "UniqueString", unique: true);
            CreateIndex("dbo.Rooms", "UniqueString", unique: true);
        }
    }
}
