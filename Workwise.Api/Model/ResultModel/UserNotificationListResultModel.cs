using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.ResultModel
{
    public class UserNotificationListResultModel
    {
        public string NotificationType { get; set; }
        public int NotificationId { get; set; }
        public UserProfile User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}
