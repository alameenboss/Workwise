using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.Data.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}