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
            _httpClient.PostData("User/ChangeNotificationStatus", notificationIds);
        }

        public async Task<UserViewModel> CreateUserProfile(string userId, string userName, string image = "")
        {
            var request = new UserViewModel()
            {
                UserId = userId,
                FirstName = userName,
                ProfilePicture = image
            };
            return await _httpClient.PostDataAsync<UserViewModel, UserViewModel>("User/CreateUserProfile", request);
        }

        public List<UserSearchViewModel> FollowersList(string UserId, string currentUserId)
        {
            return _httpClient.Get<List<UserSearchViewModel>>("User/FollowersList?UserId=" + UserId + "&currentUserId="+ currentUserId);
        }

        public List<UserSearchViewModel> FollowingList(string UserId, string currentUserId)
        {
            return _httpClient.Get<List<UserSearchViewModel>>("User/FollowingList?UserId=" + UserId + "&currentUserId=" + currentUserId);
        }

        public List<UserSearchViewModel> GetAllUsers(int count, string UserId)
        {
            return _httpClient.Get<List<UserSearchViewModel>>("User/GetAllUsers?count=" + count + "&UserId=" + UserId);
        }

        public UserProfileViewModel GetByUserId(string UserId)
        {
           return _httpClient.Get<UserProfileViewModel>("User/GetByUserId?UserId="+ UserId);
        }

        public FriendMappingViewModel GetFriendRequestStatus(string userId)
        {
            return _httpClient.Get<FriendMappingViewModel>("User/GetFriendRequestStatus?userId=" + userId);

        }

        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            return _httpClient.Get<List<OnlineUserDetailViewModel>>("User/GetFriends?userId=" + userId);
        }

        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            return _httpClient.Get<List<OnlineUserDetailViewModel>>("User/GetOnlineFriends?userId=" + userId);
        }

        public List<FriendRequestViewModel> GetReceivedFriendRequests(string userId)
        {
            return _httpClient.Get<List<FriendRequestViewModel>>("User/GetReceivedFriendRequests?userId=" + userId);

        }

        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            return _httpClient.Get<List<OnlineUserDetailViewModel>>("User/GetRecentChats?currentUserId=" + currentUserId);

        }

        public List<FriendRequestViewModel> GetSentFriendRequests(string userId)
        {
            return _httpClient.Get<List<FriendRequestViewModel>>("User/GetSentFriendRequests?userId=" + userId);

        }

        public UserProfileViewModel GetUserById(string userId)
        {
            return _httpClient.Get<UserProfileViewModel>(string.Format(Constent.User.GetUserById,userId));
        }

        public List<string> GetUserConnectionId(string UserId)
        {
            return _httpClient.Get<List<string>>("User/GetUserConnectionId?UserId=" + UserId);

        }

        public List<string> GetUsersConnectionId(string[] userIds)
        {
            return _httpClient.Get<List<string>>("User/GetUsersConnectionId?userIds=" + string.Join(",", userIds));
        }

        public int GetUserNotificationCounts(string toUserId)
        {
            return _httpClient.Get<int>("User/GetUserNotificationCounts?toUserId=" + toUserId);
        }

        public List<UserNotificationListViewModel> GetUserNotifications(string toUserId)
        {
            return _httpClient.Get<List<UserNotificationListViewModel>>("User/GetUserNotifications?toUserId=" + toUserId);

        }

        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            return _httpClient.Get<OnlineUserDetailViewModel>("User/GetUserOnlineStatus?userId=" + userId);

        }

        //public List<UserProfileViewModel> GetUsersByLinqQuery(Expression<Func<UserProfileViewModel, bool>> where)
        //{
        //    return _httpClient.Get<List<UserProfileViewModel>>("User/GetUsersByLinqQuery?userId=" + userId);

        //}

        public List<UserProfileViewModel> MyFriendsList(string UserId)
        {
            return _httpClient.Get<List<UserProfileViewModel>>("User/MyFriendsList?UserId=" + UserId);

        }

        public FriendMappingViewModel RemoveFriendMapping(int friendMappingId)
        {
            return _httpClient.Get<FriendMappingViewModel>("User/RemoveFriendMapping?friendMappingId=" + friendMappingId);

        }

        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            return _httpClient.Get<int>(string.Format("User/ResponseToFriendRequest?friendMappingId={0}&requestResponse={1}&endUserId={2}", requestorId, requestResponse,endUserId));

        }

        public void SaveProfile(UserProfileViewModel profile)
        {
            _httpClient.PostData("User/SaveProfile", profile);
        }


        public void SaveProfileImage(UserImageViewModel model)
        {
            _httpClient.PostData("User/SaveProfileImage", model);
        }

        public void SaveUserImage(UserImageViewModel model)
        {
            _httpClient.PostData("User/SaveUserImage", model);
        }


        public int SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            var model = new UserNotificationViewModel()
            {
                NotificationType = notificationType,
                FromUserId = fromUserId,
                ToUserId = toUserId
            };

            var result =  _httpClient.PostData<UserNotificationViewModel>("User/SaveUserNotification", model);

            return result.NotificationId;
        }

        public void SaveUserOnlineStatus(OnlineUserViewModel objentity)
        {
            _httpClient.PostData("User/SaveUserOnlineStatus", objentity);
        }

        public List<UserSearchViewModel> SearchUsers(string name, string userId)
        {
            var model = new UserProfileViewModel()
            {
                FirstName = name,
                UserId = userId
            };
            return _httpClient.PostDataAsync<UserProfileViewModel,List<UserSearchViewModel>>("User/SearchUsers", model).Result;
        }

        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            var model = new FriendRequestViewModel()
            {
                UserId = loggedInUserId,
                EndUserId = endUserId
            };
            _httpClient.PostData("User/SendFriendRequest", model);
        }

        public  List<UserSearchViewModel> SerachUser(string userName)
        {
            return  _httpClient.PostDataAsync<string, List<UserSearchViewModel>>("User/SerachUser", userName).Result;
        }

        public void UpdateUserProfilePicture(string userId, string imagePath)
        {
            var model = new UserProfileViewModel()
            {
                UserId = userId,
                ImageUrl = imagePath
            };
            _httpClient.PostData("User/UpdateUserProfilePicture", model);

        }
        public List<UserProfileViewModel> GetManyDummyUser(int pageNo, int pageSize)
        {
            return _httpClient.Get<List<UserProfileViewModel>>(string.Format("User/GetManyDummyUser?pageNo={0}&pageSize={1}", pageNo, pageSize));

        }

        public List<UserViewModel> GetManyUser(int pageNo, int take)
        {
            return _httpClient.Get<List<UserViewModel>>(string.Format("User/GetManyUser?pageNo={0}&take={1}", pageNo, take));

        }
    }
}
