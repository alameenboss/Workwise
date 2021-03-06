﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workwise.ViewModel
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}