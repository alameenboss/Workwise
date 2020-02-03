namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Post_Id", c => c.Int());
            AddColumn("dbo.UserProfiles", "Post_Id1", c => c.Int());
            CreateIndex("dbo.UserProfiles", "Post_Id");
            CreateIndex("dbo.UserProfiles", "Post_Id1");
            AddForeignKey("dbo.UserProfiles", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.UserProfiles", "Post_Id1", "dbo.Posts", "Id");
            DropColumn("dbo.Posts", "HasCommmet");
            DropColumn("dbo.Posts", "LikeCount");
            DropColumn("dbo.Posts", "ViewCount");
            DropColumn("dbo.Posts", "CommentCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "CommentCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "HasCommmet", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.UserProfiles", "Post_Id1", "dbo.Posts");
            DropForeignKey("dbo.UserProfiles", "Post_Id", "dbo.Posts");
            DropIndex("dbo.UserProfiles", new[] { "Post_Id1" });
            DropIndex("dbo.UserProfiles", new[] { "Post_Id" });
            DropColumn("dbo.UserProfiles", "Post_Id1");
            DropColumn("dbo.UserProfiles", "Post_Id");
        }
    }
}
