namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatinguserprofile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfileUserProfiles",
                c => new
                    {
                        UserProfile_UserId = c.String(nullable: false, maxLength: 128),
                        UserProfile_UserId1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserProfile_UserId, t.UserProfile_UserId1 })
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_UserId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_UserId1)
                .Index(t => t.UserProfile_UserId)
                .Index(t => t.UserProfile_UserId1);
            
            AddColumn("dbo.Posts", "UserProfile_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "UserProfile_UserId");
            AddForeignKey("dbo.Posts", "UserProfile_UserId", "dbo.UserProfiles", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserProfile_UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId1", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId", "dbo.UserProfiles");
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId1" });
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId" });
            DropIndex("dbo.Posts", new[] { "UserProfile_UserId" });
            DropColumn("dbo.Posts", "UserProfile_UserId");
            DropTable("dbo.UserProfileUserProfiles");
        }
    }
}
