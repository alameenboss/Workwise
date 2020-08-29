using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.Model
{
    public class Post
    {
        public Post()
        {
            Skills = new List<string>();
            Tags = new List<Tags>();
            PostImages = new List<ImageModel>();
            //PostedBy = new UserProfile();
            Comments = new List<Comment>();
            LikedBy = new List<UserProfile>();
            ViewedBy = new List<UserProfile>();
        }

        public int Id { get; set; }
        public WorkType Worktype { get; set; }
        public decimal Rate { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public string PostedById { get; set; }
        ////public virtual UserProfile PostedBy { get; set; }

        public List<UserProfile> LikedBy { get; set; }
        public List<UserProfile> ViewedBy { get; set; }

        public List<Comment> Comments { get; set; }
        public List<string> Skills { get; set; }
        public List<Tags> Tags { get; set; }
        public List<ImageModel> PostImages { get; set; }

        public bool HasCommmet
        {
            get
            {
                return Comments.Any();
            }
        }
        public int LikeCount
        {
            get
            {
                return LikedBy.Any() ? LikedBy.Count() : 0;
            }
        }
        public int ViewCount
        {
            get
            {
                return ViewedBy.Any() ? ViewedBy.Count() : 0;
            }
        }
        public int CommentCount
        {
            get
            {
                return Comments.Any() ? Comments.Count() : 0;
            }
        }

    }
}