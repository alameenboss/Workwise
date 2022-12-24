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
            _httpClient.PostDataAsync("User/ChangeNotificationStatus", notificationIds);
        }

        public UserViewModel CreateUserProfile(string userId, string userName, string image = "")
        {
            var request = new UserViewModel()
            {
                UserId = userId,
                FirstName = userName,
                ProfilePicture = image
            };
            return _httpClient.PostDataAsync<UserViewModel, UserViewModel>("User/CreateUserProfile", request);
        }

        public List<UserSearchViewModel> FollowersList(string UserId, string currentUserId)
        {
            return _httpClient.GetAsync<List<UserSearchViewModel>>("User/FollowersList?UserId=" + UserId + "&currentUserId="+ currentUserId);
        }

        public List<UserSearchViewModel> FollowingList(string UserId, string currentUserId)
        {
            return _httpClient.GetAsync<List<UserSearchViewModel>>("User/FollowingList?UserId=" + UserId + "&currentUserId=" + currentUserId);
        }

        public List<UserSearchViewModel> GetAllUsers(int count, string UserId)
        {
            return _httpClient.GetAsync<List<UserSearchViewModel>>("User/GetAllUsers?count=" + count + "&UserId=" + UserId);
        }

        public UserProfileViewModel GetByUserId(string UserId)
        {
           return _httpClient.GetAsync<UserProfileViewModel>("User/GetByUserId?UserId="+ UserId);
        }

        public FriendMappingViewModel GetFriendRequestStatus(string userId)
        {
            return _httpClient.GetAsync<FriendMappingViewModel>("User/GetFriendRequestStatus?userId=" + userId);

        }

        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            return _httpClient.GetAsync<List<OnlineUserDetailViewModel>>("User/GetFriends?userId=" + userId);
        }

        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            return _httpClient.GetAsync<List<OnlineUserDetailViewModel>>("User/GetOnlineFriends?userId=" + userId);
        }

        public List<FriendRequestViewModel> GetReceivedFriendRequests(string userId)
        {
            return _httpClient.GetAsync<List<FriendRequestViewModel>>("User/GetReceivedFriendRequests?userId=" + userId);

        }

        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            return _httpClient.GetAsync<List<OnlineUserDetailViewModel>>("User/GetRecentChats?currentUserId=" + currentUserId);

        }

        public List<FriendRequestViewModel> GetSentFriendRequests(string userId)
        {
            return _httpClient.GetAsync<List<FriendRequestViewModel>>("User/GetSentFriendRequests?userId=" + userId);

        }

        public UserProfileViewModel GetUserById(string userId)
        {
            return _httpClient.GetAsync<UserProfileViewModel>(string.Format(Constent.User.GetUserById,userId));
        }

        public List<string> GetUserConnectionId(string UserId)
        {
            return _httpClient.GetAsync<List<string>>("User/GetUserConnectionId?UserId=" + UserId);

        }

        public List<string> GetUsersConnectionId(string[] userIds)
        {
            return _httpClient.GetAsync<List<string>>("User/GetUsersConnectionId?userIds=" + string.Join(",", userIds));
        }

        public int GetUserNotificationCounts(string toUserId)
        {
            return _httpClient.GetAsync<int>("User/GetUserNotificationCounts?toUserId=" + toUserId);
        }

        public List<UserNotificationListViewModel> GetUserNotifications(string toUserId)
        {
            return _httpClient.GetAsync<List<UserNotificationListViewModel>>("User/GetUserNotifications?toUserId=" + toUserId);

        }

        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            return _httpClient.GetAsync<OnlineUserDetailViewModel>("User/GetUserOnlineStatus?userId=" + userId);

        }

        //public List<UserProfileViewModel> GetUsersByLinqQuery(Expression<Func<UserProfileViewModel, bool>> where)
        //{
        //    return  _httpClient.Get<List<UserProfileViewModel>>("User/GetUsersByLinqQuery?userId=" + userId);

        //}

        public List<UserProfileViewModel> MyFriendsList(string UserId)
        {
            return _httpClient.GetAsync<List<UserProfileViewModel>>("User/MyFriendsList?UserId=" + UserId);

        }

        public FriendMappingViewModel RemoveFriendMapping(int friendMappingId)
        {
            return _httpClient.GetAsync<FriendMappingViewModel>("User/RemoveFriendMapping?friendMappingId=" + friendMappingId);

        }

        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            return _httpClient.GetAsync<int>(string.Format("User/ResponseToFriendRequest?friendMappingId={0}&requestResponse={1}&endUserId={2}", requestorId, requestResponse,endUserId));

        }

        public void SaveProfile(UserProfileViewModel profile)
        {
            _httpClient.PostDataAsync("User/SaveProfile", profile);
        }


        public void SaveProfileImage(UserImageViewModel model)
        {
            _httpClient.PostDataAsync("User/SaveProfileImage", model);
        }

        public void SaveUserImage(UserImageViewModel model)
        {
            _httpClient.PostDataAsync("User/SaveUserImage", model);
        }


        public int SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            var model = new UserNotificationViewModel()
            {
                NotificationType = notificationType,
                FromUserId = fromUserId,
                ToUserId = toUserId
            };

            var result = _httpClient.PostDataAsync<UserNotificationViewModel>("User/SaveUserNotification", model);

            return result.NotificationId;
        }

        public void SaveUserOnlineStatus(OnlineUserViewModel objentity)
        {
            _httpClient.PostDataAsync("User/SaveUserOnlineStatus", objentity);
        }

        public List<UserSearchViewModel> SearchUsers(string name, string userId)
        {
            var model = new UserProfileViewModel()
            {
                FirstName = name,
                UserId = userId
            };
            return _httpClient.PostDataAsync<UserProfileViewModel,List<UserSearchViewModel>>("User/SearchUsers", model);
        }

        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            var model = new FriendRequestViewModel()
            {
                UserId = loggedInUserId,
                EndUserId = endUserId
            };
            _httpClient.PostDataAsync("User/SendFriendRequest", model);
        }

        public List<UserSearchViewModel> SerachUser(string userName)
        {
            return _httpClient.PostDataAsync<string, List<UserSearchViewModel>>("User/SerachUser", userName);
        }

        public void UpdateUserProfilePicture(string userId, string imagePath)
        {
            var model = new UserProfileViewModel()
            {
                UserId = userId,
                ImageUrl = imagePath
            };
            _httpClient.PostDataAsync("User/UpdateUserProfilePicture", model);

        }
        public List<UserProfileViewModel> GetManyDummyUser(int pageNo, int pageSize)
        {
            return _httpClient.GetAsync<List<UserProfileViewModel>>(string.Format("User/GetManyDummyUser?pageNo={0}&pageSize={1}", pageNo, pageSize));

        }

        public List<UserViewModel> GetManyUser(int pageNo, int take)
        {
            return _httpClient.GetAsync<List<UserViewModel>>(string.Format("User/GetManyUser?pageNo={0}&take={1}", pageNo, take));

        }
    }
}
