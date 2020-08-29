namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatinguserprofile2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId1", "dbo.UserProfiles");
            DropForeignKey("dbo.Posts", "UserProfile_UserId", "dbo.UserProfiles");
            DropIndex("dbo.Posts", new[] { "UserProfile_UserId" });
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId" });
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId1" });
            DropColumn("dbo.Posts", "UserProfile_UserId");
            DropTable("dbo.UserProfileUserProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfileUserProfiles",
                c => new
                    {
                        UserProfile_UserId = c.String(nullable: false, maxLength: 128),
                        UserProfile_UserId1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserProfile_UserId, t.UserProfile_UserId1 });
            
            AddColumn("dbo.Posts", "UserProfile_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserProfileUserProfiles", "UserProfile_UserId1");
            CreateIndex("dbo.UserProfileUserProfiles", "UserProfile_UserId");
            CreateIndex("dbo.Posts", "UserProfile_UserId");
            AddForeignKey("dbo.Posts", "UserProfile_UserId", "dbo.UserProfiles", "UserId");
            AddForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId1", "dbo.UserProfiles", "UserId");
            AddForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId", "dbo.UserProfiles", "UserId");
        }
    }
}
