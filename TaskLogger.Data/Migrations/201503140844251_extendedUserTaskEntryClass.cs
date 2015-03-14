namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendedUserTaskEntryClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskEntries", "HoursWorked", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserTasks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTasks", "Name", c => c.String());
            DropColumn("dbo.UserTaskEntries", "HoursWorked");
        }
    }
}
