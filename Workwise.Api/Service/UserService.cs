using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.ResultModel;
using Workwise.Service.Interface;

namespace Workwise.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userProfileRepo;
      
        public UserService(IUserRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
        }

        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            _userProfileRepo.SaveUserOnlineStatus(objentity);
        }
        public List<string> GetUserConnectionId(string UserId)
        {
            return _userProfileRepo.GetUserConnectionId(UserId);
        }
        public List<string> GetUserConnectionId(string[] userIds)
        {
            return _userProfileRepo.GetUserConnectionId(userIds);
        }
        public List<UserSearchResultModel> GetAllUsers(int count, string userId)
        {
            return _userProfileRepo.GetAllUsers(count,userId);
        }
        public List<UserProfile> MyFriendsList(string userId)
        {
            return _userProfileRepo.MyFriendsList(userId);
        }
        public List<UserSearchResultModel> FollowersList(string userId,string currentUserId)
        {
            return _userProfileRepo.FollowersList(userId,currentUserId);
        }
        public List<UserSearchResultModel> FollowingList(string userId, string currentUserId)
        {
            return _userProfileRepo.FollowingList(userId, currentUserId);
        }
        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            return _userProfileRepo.GetOnlineFriends(userId);
        }
        public UserProfile GetUserById(string userId)
        {
            return _userProfileRepo.GetUserById(userId);
        }
        public string[] GetFriendUserIds(string userId)
        {
            return _userProfileRepo.GetFriendUserIds(userId);
        }
        public List<FriendRequestResultModel> GetSentFriendRequests(string userId)
        {
            return _userProfileRepo.GetSentFriendRequests(userId);
        }
        public List<FriendRequestResultModel> GetReceivedFriendRequests(string userId)
        {
            return _userProfileRepo.GetReceivedFriendRequests(userId);
        }
        public List<FriendRequestResultModel> GetAllSentFriendRequests()
        {
            return _userProfileRepo.GetAllSentFriendRequests();
        }
        public List<UserSearchResultModel> SearchUsers(string name, string userId)
        {
            return _userProfileRepo.SearchUsers(name,userId);
        }
        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            _userProfileRepo.SendFriendRequest(endUserId, loggedInUserId);
        }
        public UserNotification SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            return _userProfileRepo.SaveUserNotification(notificationType, fromUserId,toUserId);
        }
        public FriendMapping GetFriendRequestStatus(string userId)
        {
             return _userProfileRepo.GetFriendRequestStatus(userId);
        }
        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            return _userProfileRepo.ResponseToFriendRequest(requestorId, requestResponse, endUserId);
        }
        public List<UserNotificationListResultModel> GetUserNotifications(string toUserId)
        {
            return _userProfileRepo.GetUserNotifications(toUserId);
        }
        public int GetUserNotificationCounts(string toUserId)
        {
            return _userProfileRepo.GetUserNotificationCounts(toUserId);
        }
        public void ChangeNotificationStatus(int[] notificationIds)
        {
            _userProfileRepo.ChangeNotificationStatus(notificationIds);
        }
        public FriendMapping RemoveFriendMapping(int friendMappingId)
        {
            return _userProfileRepo.RemoveFriendMapping(friendMappingId);
        }
       
        public List<UserProfile> GetUsersByLinqQuery(Expression<Func<UserProfile, bool>> where)
        {
            return _userProfileRepo.GetUsersByLinqQuery(where);
        }
        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            return _userProfileRepo.GetRecentChats(currentUserId);
        }
        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            return _userProfileRepo.GetUserOnlineStatus(userId);
        }
        public void SaveProfileImage(string userId, string imagePath)
        {
            _userProfileRepo.SaveProfileImage(userId, imagePath);
        }
        public void SaveUserImage(string userId, string imagePath, bool isProfilePicture)
        {
            _userProfileRepo.SaveUserImage(userId, imagePath, isProfilePicture);
        }
        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            return _userProfileRepo.GetFriends(userId);
        }
        public UserProfile GetByUserId(string UserId)
        {
          return  _userProfileRepo.GetByUserId(UserId);
        }
       
        public void SaveProfile(UserProfile profile)
        {
            _userProfileRepo.SaveProfile(profile);
        }
       
        public async Task CreateUserProfileAsync(string userId, string userName,string userImage="")
        {
            await _userProfileRepo.CreateUserProfileAsync(userId, userName);
        }
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            return _userProfileRepo.SerachUser(userName);
        }

    }
}