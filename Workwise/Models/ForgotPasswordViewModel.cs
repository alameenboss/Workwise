using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workwise.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
