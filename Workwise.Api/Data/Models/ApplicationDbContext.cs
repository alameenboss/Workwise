using System.Data.Entity;
using Workwise.Model.Configuration;

namespace Workwise.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
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
            modelBuilder.Configurations.Add(new UserProfileConfigMap());

            modelBuilder.Configurations.Add(new PostConfigMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}