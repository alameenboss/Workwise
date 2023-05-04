using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.ViewModel
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Skills = new List<string>();
            Tags = new List<TagsViewModel>();
            PostImages = new List<ImageViewModel>();
            PostedBy = new UserProfileViewModel();
            Comments = new List<CommentViewModel>();
            LikedBy = new List<UserProfileViewModel>();
            ViewedBy = new List<UserProfileViewModel>();
        }

        public int Id { get; set; }
        public WorkType Worktype { get; set; }
        public decimal Rate { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public string PostedById { get; set; }
        public virtual UserProfileViewModel PostedBy { get; set; }

        public List<UserProfileViewModel> LikedBy { get; set; }
        public List<UserProfileViewModel> ViewedBy { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public List<string> Skills { get; set; }
        public List<TagsViewModel> Tags { get; set; }
        public List<ImageViewModel> PostImages { get; set; }

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