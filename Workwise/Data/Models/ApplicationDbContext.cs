using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Workwise.Data.Models.Configuration;

namespace Workwise.Data.Models
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

        //public DbSet<User> Users { get; set; }
        public DbSet<OnlineUser> OnlineUsers { get; set; }
        public DbSet<FriendMapping> FriendMappings { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserImage> UserImages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfigMap());
            modelBuilder.Configurations.Add(new UserProfileConfigMap());

            modelBuilder.Configurations.Add(new PostConfigMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}