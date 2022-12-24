using Workwise.API.Models;

namespace Workwise.API.Model.ResultModel
{
    public class UserNotificationListResultModel
    {
        public string? NotificationType { get; set; }
        public int NotificationId { get; set; }
        public UserProfile? User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}
