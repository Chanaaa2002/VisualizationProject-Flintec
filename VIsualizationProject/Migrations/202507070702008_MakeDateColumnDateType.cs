namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateColumnDateType : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Safety_Summary");
            AlterColumn("dbo.Safety_Summary", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AddPrimaryKey("dbo.Safety_Summary", "Date");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Safety_Summary");
            AlterColumn("dbo.Safety_Summary", "Date", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Safety_Summary", "Date");
        }
    }
}
