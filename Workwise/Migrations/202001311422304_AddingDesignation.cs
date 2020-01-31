namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDesignation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Designation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Designation");
        }
    }
}
