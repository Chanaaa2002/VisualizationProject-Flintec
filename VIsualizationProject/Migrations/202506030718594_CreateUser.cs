namespace VIsualizationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "UserVisuals");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserVisuals", newName: "Users");
        }
    }
}
