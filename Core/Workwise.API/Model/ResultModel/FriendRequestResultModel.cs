using Workwise.API.Models;

namespace Workwise.API.Model.ResultModel
{
    public class FriendRequestResultModel
    {
        public UserProfile? UserInfo { get; set; }
        public string? RequestStatus { get; set; }
        public string? UserId { get; set; }
        public string? EndUserId { get; set; }
    }
}
