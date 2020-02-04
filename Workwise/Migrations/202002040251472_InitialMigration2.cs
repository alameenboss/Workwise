namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Gender", c => c.String());
            AddColumn("dbo.UserProfiles", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfiles", "Bio", c => c.String());
            AddColumn("dbo.UserProfiles", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfiles", "UpdatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfiles", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ChatMessages", "FromUserId", c => c.String());
            AlterColumn("dbo.ChatMessages", "ToUserId", c => c.String());
            AlterColumn("dbo.FriendMappings", "RequestorUserId", c => c.String());
            AlterColumn("dbo.FriendMappings", "EndUserId", c => c.String());
            AlterColumn("dbo.OnlineUsers", "UserId", c => c.String());
            AlterColumn("dbo.UserImages", "UserId", c => c.String());
            AlterColumn("dbo.UserNotifications", "ToUserId", c => c.String());
            AlterColumn("dbo.UserNotifications", "FromUserId", c => c.String());
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ProfilePicture = c.String(),
                        Gender = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Bio = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            AlterColumn("dbo.UserNotifications", "FromUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserNotifications", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserImages", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.OnlineUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendMappings", "EndUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendMappings", "RequestorUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatMessages", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatMessages", "FromUserId", c => c.Int(nullable: false));
            DropColumn("dbo.UserProfiles", "IsActive");
            DropColumn("dbo.UserProfiles", "UpdatedOn");
            DropColumn("dbo.UserProfiles", "CreatedOn");
            DropColumn("dbo.UserProfiles", "Bio");
            DropColumn("dbo.UserProfiles", "DOB");
            DropColumn("dbo.UserProfiles", "Gender");
        }
    }
}
