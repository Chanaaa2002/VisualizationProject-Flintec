namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start_Date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "Start_Date", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Announcements", "End_Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcements", "End_Date");
            DropColumn("dbo.Announcements", "Start_Date");
        }
    }
}
