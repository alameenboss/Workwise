namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Worktype = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasCommmet = c.Boolean(nullable: false),
                        Title = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        LikeCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.ImageModels", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "PostedBy_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentedBy_Id", "dbo.UserProfiles");
            DropIndex("dbo.Tags", new[] { "Post_Id" });
            DropIndex("dbo.ImageModels", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "CommentedBy_Id" });
            DropIndex("dbo.Posts", new[] { "PostedBy_Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.ImageModels");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
        }
    }
}
