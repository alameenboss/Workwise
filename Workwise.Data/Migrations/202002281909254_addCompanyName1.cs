namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompanyName1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "CompanyId", c => c.String());
        }
    }
}
