namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDisplayedToAnnouncements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "IsDisplayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Announcements", "IsDisplayed");
        }
    }
}
