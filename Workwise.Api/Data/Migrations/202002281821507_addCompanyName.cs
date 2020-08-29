namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompanyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "CompanyId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "CompanyId");
        }
    }
}
