using Workwise.API.Models;

namespace Workwise.API.Model.ResultModel
{
    public class UserSearchResultModel
    {
        public UserProfile? UserInfo { get; set; }
        public string? FriendRequestStatus { get; set; }
        public bool IsRequestReceived { get; set; }
    }
}
