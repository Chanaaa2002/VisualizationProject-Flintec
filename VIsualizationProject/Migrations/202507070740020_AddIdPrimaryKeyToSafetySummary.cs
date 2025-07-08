namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdPrimaryKeyToSafetySummary : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Safety_Summary");
            AddColumn("dbo.Safety_Summary", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Safety_Summary", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Safety_Summary");
            DropColumn("dbo.Safety_Summary", "Id");
            AddPrimaryKey("dbo.Safety_Summary", "Date");
        }
    }
}
