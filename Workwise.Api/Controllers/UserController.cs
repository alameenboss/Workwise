using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Workwise.Model;
using Workwise.ResultModel;
using Workwise.Service.Interface;

namespace Workwise.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService = null;
        private readonly IRandomUserService _randomUserService = null;
        public UserController(IUserService userService, IRandomUserService randomUserService)
        {
            _userService = userService;
            _randomUserService = randomUserService;
        }
        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            _userService.SaveUserOnlineStatus(objentity);
        }
        public List<string> GetUserConnectionId(string UserId)
        {
            return _userService.GetUserConnectionId(UserId);
        }
        public List<string> GetUserConnectionId(string[] userIds)
        {
            return _userService.GetUserConnectionId(userIds);
        }
        public List<UserProfile> GetAllUsers(int count, string userId)
        {
            return _userService.GetAllUsers(count, userId);
        }
        public List<UserProfile> MyFriendsList(string userId)
        {
            return _userService.MyFriendsList(userId);
        }
        public List<UserSearchResultModel> FollowersList(string userId, string currentUserId)
        {
            return _userService.FollowersList(userId, currentUserId);
        }
        public List<UserSearchResultModel> FollowingList(string userId, string currentUserId)
        {
            return _userService.FollowingList(userId, currentUserId);
        }
        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            return _userService.GetOnlineFriends(userId);
        }
        public UserProfile GetUserById(string userId)
        {
            return _userService.GetUserById(userId);
        }
        public string[] GetFriendUserIds(string userId)
        {
            return _userService.GetFriendUserIds(userId);
        }
        public List<FriendRequestResultModel> GetSentFriendRequests(string userId)
        {
            return _userService.GetSentFriendRequests(userId);
        }
        public List<FriendRequestResultModel> GetReceivedFriendRequests(string userId)
        {
            return _userService.GetReceivedFriendRequests(userId);
        }
        public List<FriendRequestResultModel> GetAllSentFriendRequests()
        {
            return _userService.GetAllSentFriendRequests();
        }
        public List<UserSearchResultModel> SearchUsers(string name, string userId)
        {
            return _userService.SearchUsers(name, userId);
        }
        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            _userService.SendFriendRequest(endUserId, loggedInUserId);
        }
        public int SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            return _userService.SaveUserNotification(notificationType, fromUserId, toUserId);
        }
        public FriendMapping GetFriendRequestStatus(string userId)
        {
            return _userService.GetFriendRequestStatus(userId);
        }
        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            return _userService.ResponseToFriendRequest(requestorId, requestResponse, endUserId);
        }
        public List<UserNotificationListResultModel> GetUserNotifications(string toUserId)
        {
            return _userService.GetUserNotifications(toUserId);
        }
        public int GetUserNotificationCounts(string toUserId)
        {
            return _userService.GetUserNotificationCounts(toUserId);
        }


        public void ChangeNotificationStatus(int[] notificationIds)
        {
            _userService.ChangeNotificationStatus(notificationIds);
        }
        public FriendMapping RemoveFriendMapping(int friendMappingId)
        {
            return _userService.RemoveFriendMapping(friendMappingId);
        }
      
        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            return _userService.GetRecentChats(currentUserId);
        }
        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            return _userService.GetUserOnlineStatus(userId);
        }
        public void UpdateUserProfilePicture(string userId, string imagePath)
        {
            _userService.UpdateUserProfilePicture(userId, imagePath);
        }
        public void SaveUserImage(string userId, string imagePath, bool isProfilePicture)
        {
            _userService.SaveUserImage(userId, imagePath, isProfilePicture);
        }
        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            return _userService.GetFriends(userId);
        }
        public UserProfile GetByUserId(string UserId)
        {
            return _userService.GetByUserId(UserId);
        }
        public void SaveUserImage(string userid, string imgPath)
        {
            _userService.SaveUserImage(userid, imgPath);
        }
        public void SaveProfile(UserProfile profile)
        {
            _userService.SaveProfile(profile);
        }

        public async Task CreateUserProfileAsync(string userId, string userName, string userImage = "")
        {
            await _userService.CreateUserProfileAsync(userId, userName);
        }
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            return _userService.SerachUser(userName);
        }

        public List<UserProfile> GetManyDummyUser(int pageNo, int pageSize)
        {
            return _randomUserService.GetManyDummyUser(pageNo, pageSize);
        }
    }
}
