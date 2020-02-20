using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Workwise.Model
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Bio { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Post> Posts { get; set; }

       
        public virtual List<UserProfile> Following { get; set; }

        public virtual List<UserProfile> Followers { get; set; }
    }
}