using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public UserProfileViewModel CommentedBy { get; set; }
    }
}