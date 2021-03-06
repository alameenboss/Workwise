﻿namespace Workwise.Migrations
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
                        FromUserId = c.String(),
                        ToUserId = c.String(),
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
                        UserId = c.String(),
                        EndUserId = c.String(),
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
                        UserId = c.String(),
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
                        PostedById = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CommentedBy_UserId = c.String(maxLength: 128),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.CommentedBy_UserId)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.CommentedBy_UserId)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Designation = c.String(),
                        Gender = c.String(),
                        DOB = c.DateTime(),
                        Bio = c.String(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Post_Id = c.Int(),
                        Post_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
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
                "dbo.UserImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
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
                        ToUserId = c.String(),
                        FromUserId = c.String(),
                        NotificationType = c.String(),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Post_Id1", "dbo.Posts");
            DropForeignKey("dbo.Tags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.ImageModels", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.UserProfiles", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentedBy_UserId", "dbo.UserProfiles");
            DropIndex("dbo.Tags", new[] { "Post_Id" });
            DropIndex("dbo.ImageModels", new[] { "Post_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Post_Id1" });
            DropIndex("dbo.UserProfiles", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "CommentedBy_UserId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.UserImages");
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
