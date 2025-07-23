namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePublisherPostColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "Publisher_Post", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Announcements", "Publisher_Post");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcements", "Publisher_Post", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Announcements", "Publisher_Post");
        }
    }
}
