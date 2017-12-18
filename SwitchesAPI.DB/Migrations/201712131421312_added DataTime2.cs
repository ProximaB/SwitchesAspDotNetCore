namespace SwitchesAPI.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDataTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Switches", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Switches", "CreateDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Rooms", "CreateDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
