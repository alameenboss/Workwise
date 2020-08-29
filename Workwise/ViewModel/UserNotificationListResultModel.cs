using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ViewModel
{
    public class UserNotificationListViewModel
    {
        public string NotificationType { get; set; }
        public int NotificationId { get; set; }
        public UserProfileViewModel User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}
