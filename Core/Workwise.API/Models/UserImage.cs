using System.ComponentModel.DataAnnotations;

namespace Workwise.API.Models
{
    public class UserImage
    {
        [Key]
        public int ImageId { get; set; }
        public string? UserId { get; set; }
        public string? ImagePath { get; set; }
        public bool IsProfilePicture { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
