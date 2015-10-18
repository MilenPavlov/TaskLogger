namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskEntries", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTaskEntries", "Notes");
        }
    }
}
