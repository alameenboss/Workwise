using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workwise.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
