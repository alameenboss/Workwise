using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;
using Workwise.ResultModel;

namespace Workwise.Service.Interface
{
    public interface IUserService
    {
        //Tuple<UserProfile, string> SaveUser(UserProfile objentity);
        //UserProfile CheckLogin(string userName, string password);
        void SaveUserOnlineStatus(OnlineUser objentity);
        List<string> GetUserConnectionId(string UserId);
        List<string> GetUserConnectionId(string[] userIds);
        List<UserSearchResultModel> GetAllUsers(int count, string UserId);
        List<UserProfile> MyFriendsList(string UserId);
        List<UserSearchResultModel> FollowersList(string UserId, string currentUserId);
        List<UserSearchResultModel> FollowingList(string UserId, string currentUserId);
        List<OnlineUserDetailViewModel> GetOnlineFriends(string userId);
        UserProfile GetUserById(string userId);
        List<UserSearchResultModel> SearchUsers(string name, string userId);
        List<FriendRequestResultModel> GetSentFriendRequests(string userId);
        List<FriendRequestResultModel> GetReceivedFriendRequests(string userId);
        void SendFriendRequest(string endUserId, string loggedInUserId);
        UserNotification SaveUserNotification(string notificationType, string fromUserId, string toUserId);
        FriendMapping GetFriendRequestStatus(string userId);
        int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId);
        List<UserNotificationListResultModel> GetUserNotifications(string toUserId);
        int GetUserNotificationCounts(string toUserId);
        void ChangeNotificationStatus(int[] notificationIds);
        FriendMapping RemoveFriendMapping(int friendMappingId);
        List<UserProfile> GetUsersByLinqQuery(Expression<Func<UserProfile, bool>> where);
        List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId);
        OnlineUserDetailViewModel GetUserOnlineStatus(string userId);
        void SaveProfileImage(string userId, string imagePath);
        void SaveUserImage(string userId, string imagePath, bool isProfilePicture);
        List<OnlineUserDetailViewModel> GetFriends(string userId);
        UserProfile GetByUserId(string UserId);

        void SaveProfile(UserProfile profile);
        Task CreateUserProfileAsync(string userId, string userName,string image="");
        List<UserSearchResultModel> SerachUser(string userName);
        string[] GetFriendUserIds(string userId);
        List<FriendRequestResultModel> GetAllSentFriendRequests();
    }
}