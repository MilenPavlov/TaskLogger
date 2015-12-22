namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAgeField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
        }
    }
}
