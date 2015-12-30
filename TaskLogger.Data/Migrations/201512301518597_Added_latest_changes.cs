namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_latest_changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        UserImageId = c.Guid(nullable: false),
                        ImageBytes = c.Binary(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserImageId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserImages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserImages", new[] { "UserId" });
            DropTable("dbo.UserImages");
        }
    }
}
