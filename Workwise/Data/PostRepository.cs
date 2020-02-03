using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Workwise.Data.Interface;
using Workwise.Data.Models;

namespace Workwise.Data
{
    public class PostRepository : IPostRepository
    {
        public void SavePost(Post post, string UserId)
        {
            using (var db = new ApplicationDbContext())
            {
                post.PostedBy = db.UserProfiles.FirstOrDefault(x => x.UserId == UserId);
                post.PostedOn = DateTime.Now;
                post.Location = "India";
                db.Post.Add(post);
                if (post.PostImages?.Count > 0) {
                    db.PostImages.Add(post.PostImages?.FirstOrDefault());
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Post> GetLatestPostByUser(string UserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserId == UserId);
                return db.Post.Include(x=>x.PostImages).Include(x=>x.PostedBy).Where(x=>x.PostedBy.UserId == UserId).OrderByDescending(x => x.PostedOn).Take(10).ToList();
            }
        }

    }
}