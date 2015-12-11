namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArchivedToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Archived", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserTaskEntries", "Archived", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserTasks", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "Archived");
            DropColumn("dbo.UserTaskEntries", "Archived");
            DropColumn("dbo.AspNetUsers", "Archived");
        }
    }
}
