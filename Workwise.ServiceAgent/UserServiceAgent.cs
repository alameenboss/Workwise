using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class UserServiceAgent : IUserServiceAgent
    {

        private readonly IHttpClient _httpClient;
        public UserServiceAgent(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void ChangeNotificationStatus(int[] notificationIds)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserProfileAsync(string userId, string userName, string image = "")
        {
            throw new NotImplementedException();
        }

        public List<UserSearchViewModel> FollowersList(string UserId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public List<UserSearchViewModel> FollowingList(string UserId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public List<UserProfileViewModel> GetAllUsers(int count, string UserId)
        {
            throw new NotImplementedException();
        }

        public UserProfileViewModel GetByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public FriendMappingViewModel GetFriendRequestStatus(string userId)
        {
            throw new NotImplementedException();
        }

        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            throw new NotImplementedException();
        }

        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            throw new NotImplementedException();
        }

        public List<FriendRequestViewModel> GetReceivedFriendRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            throw new NotImplementedException();
        }

        public List<FriendRequestViewModel> GetSentFriendRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public UserProfileViewModel GetUserById(string userId)
        {
            return _httpClient.Get<UserProfileViewModel>(string.Format(Constent.User.GetUserById,userId));
        }

        public List<string> GetUserConnectionId(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserConnectionId(string[] userIds)
        {
            throw new NotImplementedException();
        }

        public int GetUserNotificationCounts(string toUserId)
        {
            throw new NotImplementedException();
        }

        public List<UserNotificationListViewModel> GetUserNotifications(string toUserId)
        {
            throw new NotImplementedException();
        }

        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            throw new NotImplementedException();
        }

        public List<UserProfileViewModel> GetUsersByLinqQuery(Expression<Func<UserProfileViewModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<UserProfileViewModel> MyFriendsList(string UserId)
        {
            throw new NotImplementedException();
        }

        public FriendMappingViewModel RemoveFriendMapping(int friendMappingId)
        {
            throw new NotImplementedException();
        }

        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            throw new NotImplementedException();
        }

        public void SaveProfile(UserProfileViewModel profile)
        {
            throw new NotImplementedException();
        }

        public void SaveUserImage(string userId, string imagePath, bool isProfilePicture)
        {
            throw new NotImplementedException();
        }

        public void SaveUserImage(string userid, string imgPath)
        {
            throw new NotImplementedException();
        }

        public int SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            throw new NotImplementedException();
        }

        public void SaveUserOnlineStatus(OnlineUserViewModel objentity)
        {
            throw new NotImplementedException();
        }

        public List<UserSearchViewModel> SearchUsers(string name, string userId)
        {
            throw new NotImplementedException();
        }

        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public List<UserSearchViewModel> SerachUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserProfilePicture(string userId, string imagePath)
        {
            throw new NotImplementedException();
        }
        public List<UserProfileViewModel> GetManyDummyUser(int pageNo, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public List<UserViewModel> GetManyUser(int pageNo, int take)
        {
            throw new NotImplementedException();
        }
    }
}
