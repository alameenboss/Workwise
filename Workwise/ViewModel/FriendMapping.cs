﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ViewModel
{
    public class FriendMappingViewModel
    {
        [Key]
        public int FriendMappingId { get; set; }
        public string UserId { get; set; }
        public string EndUserId { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
