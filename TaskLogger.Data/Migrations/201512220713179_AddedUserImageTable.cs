namespace TaskLogger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        UserImageId = c.Guid(nullable: false),
                        ImageBytes = c.Binary(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserImageId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserImages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserImages", new[] { "User_Id" });
            DropTable("dbo.UserImages");
        }
    }
}
