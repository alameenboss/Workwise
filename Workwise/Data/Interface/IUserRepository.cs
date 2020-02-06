using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Workwise.Data.Models;

namespace Workwise.Data.Interface
{
    public interface IUserRepository
    {
        //Tuple<UserProfile, string> SaveUser(UserProfile objentity);
        //UserProfile CheckLogin(string userName, string password);
        void SaveUserOnlineStatus(OnlineUser objentity);
        List<string> GetUserConnectionId(string UserId);
        List<string> GetUserConnectionId(string[] userIds);
        List<UserProfile> GetAllUsers();
        List<OnlineUserDetails> GetOnlineFriends(string userId);
        UserProfile GetUserById(string userId);
        List<UserSearchResult> SearchUsers(string name, string userId);
        List<FriendRequests> GetSentFriendRequests(string userId);
        List<FriendRequests> GetReceivedFriendRequests(string userId);
        void SendFriendRequest(string endUserId, string loggedInUserId);
        int SaveUserNotification(string notificationType, string fromUserId, string toUserId);
        FriendMapping GetFriendRequestStatus(string userId);
        int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId);
        List<UserNotificationList> GetUserNotifications(string toUserId);
        int GetUserNotificationCounts(string toUserId);
        void ChangeNotificationStatus(int[] notificationIds);
        FriendMapping RemoveFriendMapping(int friendMappingId);
        List<UserProfile> GetUsersByLinqQuery(Expression<Func<UserProfile, bool>> where);
        List<OnlineUserDetails> GetRecentChats(string currentUserId);
        OnlineUserDetails GetUserOnlineStatus(string userId);
        void UpdateUserProfilePicture(string userId, string imagePath);
        void SaveUserImage(string userId, string imagePath, bool isProfilePicture);
        List<OnlineUserDetails> GetFriends(string userId);
        UserProfile GetByUserId(string UserId);
        void SaveUserImage(string userid, string imgPath);
        void SaveProfile(UserProfile profile);
        Task CreateUserProfileAsync(string userId, string userName);
    }
}