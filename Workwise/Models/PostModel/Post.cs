using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public UserProfile CommentedBy { get; set; }
    }
    public class Tags
    {
        public int Id { get; set; }
        public string Tag { get; set; }
    }
    public enum WorkType
    {
        FullTime,
        PartTime
    }
    public class Post
    {
        public int Id { get; set; }
        public WorkType Worktype { get; set; }
        public decimal Rate { get; set; }
        public List<string> Skills { get; set; }

        public bool HasCommmet { get; set; }
        public List<Tags> Tags { get; set; }

        public string Title { get; set; }
        public List<ImageModel> PostImages { get; set; }
        public DateTime PostedOn { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public UserProfile PostedBy { get; set; }

        public int LikeCount { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        public List<Comment> Comments { get; set; }

    }
}