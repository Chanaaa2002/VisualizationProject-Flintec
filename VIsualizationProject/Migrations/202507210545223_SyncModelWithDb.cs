namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncModelWithDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserVisual", "PL", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.UserVisual", "Location", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserVisual", "Location");
            DropColumn("dbo.UserVisual", "PL");
        }
    }
}
