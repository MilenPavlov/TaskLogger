namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Guid_key : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserImages");
            AlterColumn("dbo.UserImages", "UserImageId", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.UserImages", "UserImageId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserImages");
            AlterColumn("dbo.UserImages", "UserImageId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserImages", "UserImageId");
        }
    }
}
