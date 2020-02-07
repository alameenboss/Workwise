namespace Workwise.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Workwise.Data.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Workwise.Data.Models.ApplicationDbContext";
        }

        protected override void Seed(Workwise.Data.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var PasswordHash = new PasswordHasher();
            //if (!context.Users.Any(u => u.UserName == "admin@admin.net"))
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "admin@admin.net",
            //        Email = "admin@admin.net",
            //        PasswordHash = PasswordHash.HashPassword("123456")
            //    };

            //    UserManager.Create(user);
            //    UserManager.AddToRole(user.Id, Const.getRoles()[0]);
            //}

        }
    }
}
