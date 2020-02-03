using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public UserProfile CommentedBy { get; set; }
    }
}