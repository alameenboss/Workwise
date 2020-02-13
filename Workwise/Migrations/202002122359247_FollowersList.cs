namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FollowersList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "UserProfile_UserId", "dbo.UserProfiles");
            DropIndex("dbo.UserProfiles", new[] { "UserProfile_UserId" });
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
            
            DropColumn("dbo.UserProfiles", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "UserProfile_UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId1", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfileUserProfiles", "UserProfile_UserId", "dbo.UserProfiles");
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId1" });
            DropIndex("dbo.UserProfileUserProfiles", new[] { "UserProfile_UserId" });
            DropTable("dbo.UserProfileUserProfiles");
            CreateIndex("dbo.UserProfiles", "UserProfile_UserId");
            AddForeignKey("dbo.UserProfiles", "UserProfile_UserId", "dbo.UserProfiles", "UserId");
        }
    }
}
