using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent.Interface
{
    public interface IUserServiceAgent
    {
        void SaveUserOnlineStatus(OnlineUserViewModel objentity);
        List<string> GetUserConnectionId(string UserId);
        List<string> GetUserConnectionId(string[] userIds);
        List<UserSearchViewModel> GetAllUsers(int count, string UserId);
        List<UserProfileViewModel> MyFriendsList(string UserId);
        List<UserSearchViewModel> FollowersList(string UserId, string currentUserId);
        List<UserSearchViewModel> FollowingList(string UserId, string currentUserId);
        List<OnlineUserDetailViewModel> GetOnlineFriends(string userId);
        UserProfileViewModel GetUserById(string userId);
        List<UserSearchViewModel> SearchUsers(string name, string userId);
        List<FriendRequestViewModel> GetSentFriendRequests(string userId);
        List<FriendRequestViewModel> GetReceivedFriendRequests(string userId);
        void SendFriendRequest(string endUserId, string loggedInUserId);
        int SaveUserNotification(string notificationType, string fromUserId, string toUserId);
        FriendMappingViewModel GetFriendRequestStatus(string userId);
        int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId);
        List<UserNotificationListViewModel> GetUserNotifications(string toUserId);
        int GetUserNotificationCounts(string toUserId);
        void ChangeNotificationStatus(int[] notificationIds);
        FriendMappingViewModel RemoveFriendMapping(int friendMappingId);
        //List<UserProfileViewModel> GetUsersByLinqQuery(Expression<Func<UserProfileViewModel, bool>> where);
        List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId);
        OnlineUserDetailViewModel GetUserOnlineStatus(string userId);
        void UpdateUserProfilePicture(string userId, string imagePath);
        List<OnlineUserDetailViewModel> GetFriends(string userId);
        UserProfileViewModel GetByUserId(string UserId);
        void SaveProfileImage(UserImageViewModel model);
        void SaveProfile(UserProfileViewModel profile);
        void CreateUserProfile(string userId, string userName, string image = "");
        List<UserSearchViewModel> SerachUser(string userName);

        List<UserProfileViewModel> GetManyDummyUser(int pageNo, int pageSize);
        List<UserViewModel> GetManyUser(int pageNo, int take);
    }
}
