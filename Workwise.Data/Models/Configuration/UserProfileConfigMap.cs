using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Workwise.Model.Configuration
{
    public class UserProfileConfigMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfigMap()
        {
            HasMany(x => x.Posts)
                .WithRequired(x => x.PostedBy)
                .HasForeignKey(x => x.PostedById);

            //HasMany(u => u.Following)
            //    .WithMany()
            //    .Map(x => {
            //        x.MapLeftKey("UserId");
            //        x.MapRightKey("FriendId");
            //        x.ToTable("FollowerMapping");
            //    });
           
        }
    }
  

    public class PostConfigMap : EntityTypeConfiguration<Post>
    {
        public PostConfigMap()
        {

            HasRequired(x => x.PostedBy);
                

        }
    }
}