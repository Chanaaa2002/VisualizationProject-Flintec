namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionVisualization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Announcement_name = c.String(nullable: false, maxLength: 50),
                        Publisher = c.String(nullable: false, maxLength: 50),
                        Publisher_Post = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        Start_Date = c.DateTime(nullable: false, storeType: "date"),
                        End_Date = c.DateTime(nullable: false, storeType: "date"),
                        Production_Line = c.String(nullable: false, maxLength: 50),
                        Location = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Birthdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Employee_Number = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Position = c.String(nullable: false, maxLength: 50),
                        Production_Line = c.String(nullable: false, maxLength: 50),
                        Location = c.String(nullable: false, maxLength: 50),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Layout",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Production_Line = c.String(nullable: false, maxLength: 50),
                        Location = c.String(nullable: false, maxLength: 50),
                        LayoutImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OGQC",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detected_Date = c.DateTime(nullable: false, storeType: "date"),
                        Product = c.String(nullable: false, maxLength: 50),
                        Lot_Size = c.String(nullable: false, maxLength: 50),
                        Defect_Qty = c.String(nullable: false, maxLength: 50),
                        Production_Line = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Safety_Summary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Employee_Name = c.String(nullable: false, maxLength: 50),
                        Injury_status = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                        Production_Line = c.String(nullable: false, maxLength: 50),
                        Location = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserVisual",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Role = c.String(nullable: false, maxLength: 20),
                        PL = c.String(nullable: false, maxLength: 20),
                        Location = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserVisual");
            DropTable("dbo.Safety_Summary");
            DropTable("dbo.OGQC");
            DropTable("dbo.Layout");
            DropTable("dbo.Birthdays");
            DropTable("dbo.Announcements");
        }
    }
}
