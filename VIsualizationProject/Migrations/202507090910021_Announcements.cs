namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Announcement = c.String(nullable: false, maxLength: 50),
                        Publisher = c.String(nullable: false, maxLength: 50),
                        Piblisher_Post = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Announcements");
        }
    }
}
