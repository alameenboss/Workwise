using System.Linq.Expressions;
using Workwise.API.Data.EFCore;
using Workwise.API.Data.Interface;
using Workwise.API.Model.ResultModel;
using Workwise.API.Models;

namespace Workwise.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkwiseDbContext workwiseDbContext;

        public UserRepository(WorkwiseDbContext workwiseDbContext)
        {
            this.workwiseDbContext = workwiseDbContext;
        }
        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            OnlineUser? obj = workwiseDbContext.OnlineUsers.Where(m => m.UserId == objentity.UserId).FirstOrDefault();
            if (obj != null)
            {
                obj.IsOnline = objentity.IsOnline;
                obj.UpdatedOn = DateTime.Now;
                obj.ConnectionId = objentity.ConnectionId;
            }
            else
            {
                objentity.CreatedOn = DateTime.Now;
                objentity.UpdatedOn = DateTime.Now;
                objentity.IsActive = true;
                _ = workwiseDbContext.OnlineUsers.Add(objentity);
            }
            _ = workwiseDbContext.SaveChanges();
        }
        public List<string> GetUserConnectionId(string UserId)
        {
            List<string> obj = workwiseDbContext.OnlineUsers.Where(m => m.UserId == UserId && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionId).ToList();
            return obj;
        }
        public List<string> GetUserConnectionId(string[] userIds)
        {
            List<string> obj = workwiseDbContext.OnlineUsers.Where(m => userIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionId).ToList();
            return obj;
        }
        public List<UserSearchResultModel> GetAllUsers(int count, string userId)
        {
            List<UserSearchResultModel> v = new();
            v.AddRange(FollowersList(userId, userId));
            v.AddRange(FollowingList(userId, userId));
            List<string?> ids = v.Select(x => x.UserInfo.UserId).ToList();

            List<UserSearchResultModel> list = workwiseDbContext.UserProfiles
                .Where(x => !ids.Contains(x.UserId))
                .Take(count).Select(x =>
                    new UserSearchResultModel()
                    {
                        UserInfo = x,
                        FriendRequestStatus = "Follow"
                    })
                .ToList();
            return list;
        }
        public List<UserProfile> MyFriendsList(string userId)
        {

            var following = workwiseDbContext.FriendMappings.Where(m => m.UserId == userId).Select(x => new { id = x.EndUserId });
            var follower = workwiseDbContext.FriendMappings.Where(m => m.EndUserId == userId).Select(x => new { id = x.UserId });
            List<string?> myfriends = following.Union(follower).ToList().Select(x => x.id).ToList();

            return workwiseDbContext.UserProfiles.Where(x => myfriends.Contains(x.UserId)).ToList();
        }
        public List<UserSearchResultModel> FollowersList(string userId, string currentUserId)
        {

            List<UserSearchResultModel> myFollowers = (from f in workwiseDbContext.FriendMappings
                                                       join up in workwiseDbContext.UserProfiles on f.UserId equals up.UserId
                                                       where f.EndUserId == currentUserId
                                                       select new UserSearchResultModel
                                                       {
                                                           UserInfo = up,
                                                           FriendRequestStatus = f.RequestStatus
                                                       }
                              ).ToList();



            List<UserSearchResultModel> userFollower;
            if (userId == currentUserId)
            {
                userFollower = myFollowers;
            }
            else
            {
                userFollower = (from f in workwiseDbContext.FriendMappings
                                join up in workwiseDbContext.UserProfiles on f.UserId equals up.UserId
                                where f.EndUserId == userId && f.RequestStatus == "Accepted"
                                select new UserSearchResultModel
                                {
                                    UserInfo = up,
                                    FriendRequestStatus = f.RequestStatus
                                }
                               ).ToList();
                userFollower.ForEach(x =>
                {
                    UserSearchResultModel? friend = myFollowers.FirstOrDefault(y => y == x);
                    x.FriendRequestStatus = friend != null ? friend.FriendRequestStatus : "Follow";


                });


            }


            userFollower.ForEach(x =>
            {
                x.FriendRequestStatus = x.FriendRequestStatus == "Follow" ? "Follow" :
                x.FriendRequestStatus == "Accepted" ? "Following" : "Pending";
            });

            return userFollower;

        }
        public List<UserSearchResultModel> FollowingList(string userId, string currentUserId)
        {
            List<UserSearchResultModel> followingList = (from f in workwiseDbContext.FriendMappings
                                                         join up in workwiseDbContext.UserProfiles on f.EndUserId equals up.UserId
                                                         where f.UserId == userId
                                                         select new UserSearchResultModel
                                                         {
                                                             UserInfo = up,
                                                             FriendRequestStatus = f.RequestStatus
                                                         }).ToList();

            return followingList;
        }
        public List<OnlineUserDetailViewModel> GetOnlineFriends(string userId)
        {
            string[] friends = GetFriendUserIds(userId);
            List<OnlineUser> friendOnlineDetails = workwiseDbContext.OnlineUsers.Where(m => friends.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).ToList();
            List<OnlineUserDetailViewModel> obj = (from v in workwiseDbContext.UserProfiles
                                                   where friends.Contains(v.UserId)
                                                   select new OnlineUserDetailViewModel
                                                   {
                                                       UserId = v.UserId,
                                                       Name = v.FirstName,
                                                       ProfilePicture = v.ImageUrl,
                                                       Gender = v.Gender
                                                   }).OrderBy(m => m.Name).ToList();
            string[] onlineUserIds = friendOnlineDetails.Select(m => m.UserId).ToArray();
            obj = obj.Where(m => onlineUserIds.Contains(m.UserId)).ToList();
            obj.ForEach(m =>
            {
                m.ConnectionId = friendOnlineDetails.Where(x => x.UserId == m.UserId).Select(x => x.ConnectionId).ToList();
            });
            return obj;
        }
        public UserProfile GetUserById(string userId)
        {
            UserProfile? obj = workwiseDbContext.UserProfiles.Where(m => m.UserId == userId).FirstOrDefault();
            return obj;
        }
        public string[] GetFriendUserIds(string userId)
        {
            string?[] arr = workwiseDbContext.FriendMappings.Where(m => (m.UserId == userId || m.EndUserId == userId) && m.RequestStatus == "Accepted" && m.IsActive == true).Select(m => m.UserId == userId ? m.EndUserId : m.UserId).ToArray();
            return arr;
        }
        public List<FriendRequestResultModel> GetSentFriendRequests(string userId)
        {
            List<FriendRequestResultModel> list = (from u in workwiseDbContext.FriendMappings
                                                   join v in workwiseDbContext.UserProfiles on u.EndUserId equals v.UserId
                                                   where u.UserId == userId && u.RequestStatus == "Sent" && u.IsActive == true
                                                   select new FriendRequestResultModel()
                                                   {
                                                       UserInfo = v,
                                                       RequestStatus = u.RequestStatus,
                                                       EndUserId = u.EndUserId,
                                                       UserId = u.UserId
                                                   }).ToList();
            return list;
        }
        public List<FriendRequestResultModel> GetReceivedFriendRequests(string userId)
        {
            List<FriendRequestResultModel> list = (from u in workwiseDbContext.FriendMappings
                                                   join v in workwiseDbContext.UserProfiles on u.UserId equals v.UserId
                                                   where u.EndUserId == userId && u.RequestStatus == "Sent" && u.IsActive == true
                                                   select new FriendRequestResultModel()
                                                   {
                                                       UserInfo = v,
                                                       RequestStatus = u.RequestStatus,
                                                       EndUserId = u.EndUserId,
                                                       UserId = u.UserId
                                                   }).ToList();
            return list;
        }
        public List<FriendRequestResultModel> GetAllSentFriendRequests()
        {
            List<FriendRequestResultModel> list = (from u in workwiseDbContext.FriendMappings
                                                   join v in workwiseDbContext.UserProfiles on u.EndUserId equals v.UserId
                                                   where u.RequestStatus == "Sent" && u.IsActive == true
                                                   select new FriendRequestResultModel()
                                                   {
                                                       UserInfo = v,
                                                       RequestStatus = u.RequestStatus,
                                                       EndUserId = u.EndUserId,
                                                       UserId = u.UserId
                                                   }).ToList();
            return list;
        }
        public List<UserSearchResultModel> SearchUsers(string name, string userId)
        {
            string[] friendIds = GetFriendUserIds(userId);
            List<UserProfile> objList = workwiseDbContext.UserProfiles.Where(m => m.FirstName.ToLower().Contains(name.ToLower()) && m.UserId != userId && !friendIds.Contains(m.UserId)).ToList();
            List<FriendRequestResultModel> receivedRequests = GetReceivedFriendRequests(userId);
            List<FriendRequestResultModel> sentRequests = GetSentFriendRequests(userId);
            List<UserSearchResultModel> list = new();
            foreach (UserProfile? item in objList)
            {
                bool isReceived = false;
                FriendRequestResultModel? receivedRequest = receivedRequests.Where(x => x.UserInfo.UserId == item.UserId).FirstOrDefault();
                if (receivedRequest != null)
                {
                    isReceived = true;
                }
                UserSearchResultModel userInfo = new()
                {
                    IsRequestReceived = isReceived,
                    UserInfo = item
                };
                FriendRequestResultModel? sentRequest = sentRequests.Where(m => m.UserInfo.UserId == item.UserId).FirstOrDefault();
                if (sentRequest != null)
                {
                    userInfo.FriendRequestStatus = sentRequest.RequestStatus; ;
                }
                list.Add(userInfo);
            }
            return list;
        }
        public void SendFriendRequest(string endUserId, string loggedInUserId)
        {
            FriendMapping objentity = new()
            {
                CreatedOn = DateTime.Now,
                EndUserId = endUserId,
                IsActive = true,
                UserId = loggedInUserId,
                RequestStatus = "Sent",
                UpdatedOn = DateTime.Now
            };
            _ = workwiseDbContext.FriendMappings.Add(objentity);
            _ = workwiseDbContext.SaveChanges();
        }
        public UserNotification SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            UserNotification notification = new()
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                NotificationType = notificationType,
                FromUserId = fromUserId,
                Status = "New",
                UpdatedOn = DateTime.Now,
                ToUserId = toUserId
            };
            _ = workwiseDbContext.UserNotifications.Add(notification);
            _ = workwiseDbContext.SaveChanges();
            return notification;
        }
        public FriendMapping GetFriendRequestStatus(string userId)
        {
            FriendMapping? obj = workwiseDbContext.FriendMappings.Where(m => (m.EndUserId == userId || m.UserId == userId) && m.IsActive == true).FirstOrDefault();
            return obj;
        }
        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            FriendMapping? request = workwiseDbContext.FriendMappings.Where(m => m.EndUserId == endUserId && m.UserId == requestorId && m.IsActive == true).FirstOrDefault();
            if (request != null)
            {
                request.RequestStatus = requestResponse;
                request.UpdatedOn = DateTime.Now;
                _ = workwiseDbContext.SaveChanges();
            }
            UserNotification? notification = workwiseDbContext.UserNotifications.Where(m => m.ToUserId == endUserId && m.FromUserId == requestorId && m.IsActive == true && m.NotificationType == "FriendRequest").FirstOrDefault();
            if (notification != null)
            {
                notification.IsActive = false;
                notification.UpdatedOn = DateTime.Now;
                _ = workwiseDbContext.SaveChanges();
                return notification.NotificationId;
            }
            return 0;
        }
        public List<UserNotificationListResultModel> GetUserNotifications(string toUserId)
        {
            IOrderedQueryable<UserNotificationListResultModel> listQuery = (from u in workwiseDbContext.UserNotifications
                                                                            join v in workwiseDbContext.UserProfiles on u.FromUserId equals v.UserId
                                                                            where u.ToUserId == toUserId && u.IsActive == true
                                                                            select new UserNotificationListResultModel()
                                                                            {
                                                                                NotificationId = u.NotificationId,
                                                                                NotificationType = u.NotificationType,
                                                                                User = v,
                                                                                NotificationStatus = u.Status,
                                                                                CreatedOn = u.CreatedOn
                                                                            }).OrderByDescending(m => m.CreatedOn);
            int totalNotifications = listQuery.Count();
            List<UserNotificationListResultModel> list = listQuery.Take(3).ToList();
            list.ForEach(m => m.TotalNotifications = totalNotifications);
            return list;
        }
        public int GetUserNotificationCounts(string toUserId)
        {
            int count = workwiseDbContext.UserNotifications.Where(m => m.Status == "New" && m.ToUserId == toUserId && m.IsActive == true).Count();
            return count;
        }
        public void ChangeNotificationStatus(int[] notificationIds)
        {
            workwiseDbContext.UserNotifications.Where(m => notificationIds.Contains(m.NotificationId)).ToList().ForEach(m => m.Status = "Read");
            _ = workwiseDbContext.SaveChanges();
        }
        public FriendMapping RemoveFriendMapping(int friendMappingId)
        {
            FriendMapping? obj = workwiseDbContext.FriendMappings.Where(m => m.FriendMappingId == friendMappingId).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
                _ = workwiseDbContext.SaveChanges();
            }
            return obj;
        }
        public void UpdateProfilePicture(string userId, string profilePicturePath)
        {

        }
        public List<UserProfile> GetUsersByLinqQuery(Expression<Func<UserProfile, bool>> where)
        {
            List<UserProfile> objList = workwiseDbContext.UserProfiles.Where(where).ToList();
            return objList;
        }
        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            string[] friends = GetFriendUserIds(currentUserId);
            List<ChatMessage> recentMessages = workwiseDbContext.ChatMessages.Where(m => m.IsActive == true && (m.ToUserId == currentUserId || m.FromUserId == currentUserId)).OrderByDescending(m => m.CreatedOn).ToList();
            string?[] userIds = recentMessages.Select(m => m.ToUserId == currentUserId ? m.FromUserId : m.ToUserId).Distinct().ToArray();
            List<string?> userIdsList = userIds.ToList();
            List<ChatMessage> messagesByUserId = recentMessages.Where(m => m.ToUserId == currentUserId && m.Status == "Sent").ToList();
            var newMessagesCount = (from p in messagesByUserId
                                    group p by p.FromUserId into g
                                    select new { FromUserId = g.Key, Count = g.Count() }).ToList();
            string[] onlineUserIds = workwiseDbContext.OnlineUsers.Where(m => friends.Contains(m.UserId) && userIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserId).ToArray();
            List<OnlineUserDetailViewModel> users = (from m in workwiseDbContext.UserProfiles
                                                     join v in userIdsList on m.UserId equals v
                                                     select new OnlineUserDetailViewModel
                                                     {
                                                         UserId = m.UserId,
                                                         Name = m.FirstName,
                                                         ProfilePicture = m.ImageUrl,
                                                         Gender = m.Gender,
                                                         IsOnline = onlineUserIds.Contains(m.UserId)
                                                     }).ToList();
            users.ForEach(m =>
            {
                m.UnReadMessageCount = newMessagesCount.Where(x => x.FromUserId == m.UserId).Select(x => x.Count).FirstOrDefault();
            });
            users = users.OrderBy(d => userIdsList.IndexOf(d.UserId)).ToList();
            return users;
        }
        public OnlineUserDetailViewModel GetUserOnlineStatus(string userId)
        {
            OnlineUserDetailViewModel obj = new()
            {
                UserId = userId
            };
            List<OnlineUser> objList = workwiseDbContext.OnlineUsers.Where(m => m.UserId == userId && m.IsActive == true).ToList();
            if (objList != null && objList.Count > 0)
            {
                obj.IsOnline = false;
                List<OnlineUser> onlineConnections = objList.Where(m => m.IsOnline).ToList();
                List<OnlineUser> offlineConnections = objList.Where(m => !m.IsOnline).ToList();
                if (onlineConnections != null && onlineConnections.Count > 0)
                {
                    obj.IsOnline = true;
                }
                else if (offlineConnections != null && offlineConnections.Count > 0)
                {
                    obj.IsOnline = false;
                    obj.LastUpdationTime = offlineConnections.OrderByDescending(m => m.UpdatedOn).Select(m => m.UpdatedOn).FirstOrDefault();
                }
            }
            return obj;
        }
        public void SaveProfileImage(string userId, string imagePath)
        {
            UserProfile? obj = workwiseDbContext.UserProfiles.Where(m => m.UserId == userId).FirstOrDefault();
            if (obj != null)
            {
                obj.ImageUrl = imagePath;
                obj.UpdatedOn = DateTime.Now;
                _ = workwiseDbContext.SaveChanges();
                SaveUserImage(userId, imagePath, true);
            }
        }
        public void SaveUserImage(string userId, string imagePath, bool isProfilePicture)
        {
            if (isProfilePicture)
            {
                UserImage? existingProfilePicture = workwiseDbContext.UserImages.Where(m => m.UserId == userId && m.IsActive == true && m.IsProfilePicture == true).FirstOrDefault();
                if (existingProfilePicture != null)
                {
                    existingProfilePicture.IsProfilePicture = false;
                    _ = workwiseDbContext.SaveChanges();
                }
            }
            UserImage objentity = new()
            {
                CreatedOn = DateTime.Now,
                ImagePath = imagePath,
                IsActive = true,
                IsProfilePicture = isProfilePicture,
                UserId = userId
            };
            _ = workwiseDbContext.UserImages.Add(objentity);
            _ = workwiseDbContext.SaveChanges();
        }
        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            string[] friendIds = GetFriendUserIds(userId);
            string[] onlineUserIds = workwiseDbContext.OnlineUsers.Where(m => friendIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserId).ToArray();
            List<OnlineUserDetailViewModel> users = workwiseDbContext.UserProfiles.Where(m => friendIds.Contains(m.UserId)).Select(m => new OnlineUserDetailViewModel
            {
                UserId = m.UserId,
                Name = m.FirstName,
                ProfilePicture = m.ImageUrl,
                Gender = m.Gender,
                IsOnline = onlineUserIds.Contains(m.UserId)
            }).ToList();
            return users;
        }
        public UserProfile GetByUserId(string UserId)
        {
            return workwiseDbContext.UserProfiles.FirstOrDefault(x => x.UserId == UserId);

        }
        //public void SaveUserImage(string userid, string imgPath)
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var model = db.UserProfiles.FirstOrDefault(x => x.UserId == userid); ;
        //        if (model != null)
        //        {
        //            model.ImageUrl = imgPath;
        //        }
        //        else
        //        {
        //            db.UserProfiles.Add(new UserProfile()
        //            {
        //                UserId = userid,
        //                ImageUrl = imgPath
        //            });
        //        }
        //        db.SaveChanges();
        //    }
        //}
        public void SaveProfile(UserProfile profile)
        {
            UserProfile? user = workwiseDbContext.UserProfiles.FirstOrDefault(x => x.UserId == profile.UserId);

            if (user == null)
            {
                user = new UserProfile()
                {
                    UserId = profile.UserId,
                    FirstName = profile.FirstName,
                    Designation = profile.Designation
                };

                _ = workwiseDbContext.UserProfiles.Add(user);
            }

            _ = workwiseDbContext.SaveChanges();
        }
        //public List<UserProfile> GetAllUsers()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        return db.UserProfiles.ToList();

        //    }
        //}
        public async Task CreateUserProfileAsync(string userId, string userName, string userImage = "")
        {
            _ = workwiseDbContext.UserProfiles.Add(new UserProfile()
            {
                UserId = userId,
                FirstName = userName,
                ImageUrl = !string.IsNullOrEmpty(userImage) ? userImage : @"/images/DefaultPhoto.png",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                IsActive = true
            });
            _ = await workwiseDbContext.SaveChangesAsync();
        }
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            return workwiseDbContext.UserProfiles
                   .Where(x => x.FirstName.Contains(userName))
                   .Select(x => new UserSearchResultModel { UserInfo = x })
                   .ToList();
        }

    }
}