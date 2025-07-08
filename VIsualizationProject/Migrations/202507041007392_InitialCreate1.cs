namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Safety_Summary",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        Employee_Name = c.String(nullable: false, maxLength: 50),
                        Injury_status = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Date);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Safety_Summary");
        }
    }
}
