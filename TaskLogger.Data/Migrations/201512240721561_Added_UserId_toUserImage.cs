namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserId_toUserImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserImages", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserImages", "UserId");
        }
    }
}
