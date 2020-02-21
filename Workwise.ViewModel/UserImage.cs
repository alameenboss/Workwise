using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ViewModel
{
    public class UserImageViewModel
    {
        [Key]
        public int ImageId { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public bool IsProfilePicture { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
