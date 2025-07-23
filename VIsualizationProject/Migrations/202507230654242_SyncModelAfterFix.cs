namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncModelAfterFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserVisual", "PL", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.UserVisual", "Location", c => c.String(nullable: false, maxLength: 20));
            DropTable("dbo.Birthday");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Birthday",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Employee_Number = c.String(nullable: false, maxLength: 50),
                        Employee_Name = c.String(nullable: false, maxLength: 50),
                        Position = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.UserVisual", "Location", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserVisual", "PL", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
