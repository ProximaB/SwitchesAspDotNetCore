namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastModifiedDataTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Last modified DateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Switches", "Last modified DateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Rooms", "CreateDate");
            DropColumn("dbo.Switches", "CreateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Switches", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Rooms", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Switches", "Last modified DateTime");
            DropColumn("dbo.Rooms", "Last modified DateTime");
        }
    }
}
