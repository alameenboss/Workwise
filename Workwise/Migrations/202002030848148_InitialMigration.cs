namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ChatMessageId = c.Int(nullable: false, identity: true),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Message = c.String(),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        ViewedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChatMessageId);
            
            CreateTable(
                "dbo.FriendMappings",
                c => new
                    {
                        FriendMappingId = c.Int(nullable: false, identity: true),
                        RequestorUserId = c.Int(nullable: false),
                        EndUserId = c.Int(nullable: false),
                        RequestStatus = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FriendMappingId);
            
            CreateTable(
                "dbo.OnlineUsers",
                c => new
                    {
                        OnlineUserId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ConnectionId = c.String(),
                        IsOnline = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OnlineUserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Worktype = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Title = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        PostedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.PostedBy_Id)
                .Index(t => t.PostedBy_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CommentedBy_Id = c.Int(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.CommentedBy_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.CommentedBy_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ImageUrl = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Designation = c.String(),
                        Post_Id = c.Int(),
                        Post_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id1)
                .Index(t => t.Post_Id)
                .Index(t => t.Post_Id1);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ImagePath = c.String(),
                        IsProfilePicture = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        ToUserId = c.Int(nullable: false),
                        FromUserId = c.Int(nullable: false),
                        NotificationType = c.String(),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserProfiles", "Post_Id1", "dbo.Posts");
            DropForeignKey("dbo.Tags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.ImageModels", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "PostedBy_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentedBy_Id", "dbo.UserProfiles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tags", new[] { "Post_Id" });
            DropIndex("dbo.ImageModels", new[] { "Post_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Post_Id1" });
            DropIndex("dbo.UserProfiles", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "CommentedBy_Id" });
            DropIndex("dbo.Posts", new[] { "PostedBy_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.UserImages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.ImageModels");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.OnlineUsers");
            DropTable("dbo.FriendMappings");
            DropTable("dbo.ChatMessages");
        }
    }
}
