﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ViewModel
{
    public class UserNotificationViewModel
    {
        [Key]
        public int NotificationId { get; set; }
        public string ToUserId { get; set; }
        public string FromUserId { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }

       
        public UserViewModel User { get; set; }
       
        public string NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}
