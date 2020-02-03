using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Workwise.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<ImageModel> PostImages { get; set; }

        public DbSet<Tags> PostTags { get; set; }

        public DbSet<Comment> PostComment { get; set; }


    }
}