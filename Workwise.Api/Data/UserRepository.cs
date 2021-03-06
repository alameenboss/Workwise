﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.ResultModel;

namespace Workwise.Data
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public void SaveUserOnlineStatus(OnlineUser objentity)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserId == objentity.UserId).FirstOrDefault();
            if (obj != null)
            {
                obj.IsOnline = objentity.IsOnline;
                obj.UpdatedOn = System.DateTime.Now;
                obj.ConnectionId = objentity.ConnectionId;
            }
            else
            {
                objentity.CreatedOn = System.DateTime.Now;
                objentity.UpdatedOn = System.DateTime.Now;
                objentity.IsActive = true;
                _context.OnlineUsers.Add(objentity);
            }
            _context.SaveChanges();
        }
        public List<string> GetUserConnectionId(string UserId)
        {
            var obj = _context.OnlineUsers.Where(m => m.UserId == UserId && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionId).ToList();
            return obj;
        }
        public List<string> GetUserConnectionId(string[] userIds)
        {
            var obj = _context.OnlineUsers.Where(m => userIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionId).ToList();
            return obj;
        }
        public List<UserSearchResultModel> GetAllUsers(int count, string userId)
        {
            var v = new List<UserSearchResultModel>();
            v.AddRange(FollowersList(userId, userId));
            v.AddRange(FollowingList(userId, userId));
            var ids = v.Select(x => x.UserInfo.UserId).ToList();

            var list = _context.UserProfiles
                .Where(x => !ids.Contains(x.UserId))
                .Take(count).Select(x=> 
                    new UserSearchResultModel() {
                        UserInfo =  x,
                        FriendRequestStatus = "Follow"}) 
                .ToList();
            return list;
        }
        public List<UserProfile> MyFriendsList(string userId)
        {
            
            var following = _context.FriendMappings.Where(m => (m.UserId == userId)).Select(x => new { id = x.EndUserId});
            var follower = _context.FriendMappings.Where(m => (m.EndUserId == userId)).Select(x => new { id = x.UserId });
            var myfriends =  following.Union(follower).ToList().Select(x=>x.id).ToList();

           return _context.UserProfiles.Where(x => myfriends.Contains(x.UserId)).ToList();
        }
        public List<UserSearchResultModel> FollowersList(string userId,string currentUserId)
        {
           
            var myFollowers = (from f in _context.FriendMappings
                                join up in _context.UserProfiles on f.UserId equals up.UserId
                                where f.EndUserId == currentUserId
                                select new UserSearchResultModel {
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
                userFollower = (from f in _context.FriendMappings
                                join up in _context.UserProfiles on f.UserId equals up.UserId
                                where f.EndUserId == userId && f.RequestStatus == "Accepted"
                                select new UserSearchResultModel
                                {
                                    UserInfo = up,
                                    FriendRequestStatus = f.RequestStatus
                                }
                               ).ToList();
                userFollower.ForEach(x =>
                {
                    var friend = myFollowers.FirstOrDefault(y => y == x);
                    if (friend != null)
                    {
                        x.FriendRequestStatus = friend.FriendRequestStatus;
                    }
                    else
                    {
                        x.FriendRequestStatus = "Follow";
                    }

                       
                });


            }


            userFollower.ForEach(x => { x.FriendRequestStatus = x.FriendRequestStatus == "Follow" ? "Follow" :
                x.FriendRequestStatus == "Accepted" ? "Following" : "Pending"; });

            return userFollower;

        }
        public List<UserSearchResultModel> FollowingList(string userId, string currentUserId)
        {
            var followingList = (from f in _context.FriendMappings
                                 join up in _context.UserProfiles on f.EndUserId equals up.UserId 
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
            var friendOnlineDetails = _context.OnlineUsers.Where(m => friends.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).ToList();
            var obj = (from v in _context.UserProfiles
                       where friends.Contains(v.UserId)
                       select new OnlineUserDetailViewModel
                       {
                           UserId = v.UserId,
                           Name = v.FirstName,
                           ProfilePicture = v.ImageUrl,
                           Gender = v.Gender
                       }).OrderBy(m => m.Name).ToList();
            var onlineUserIds = friendOnlineDetails.Select(m => m.UserId).ToArray();
            obj = obj.Where(m => onlineUserIds.Contains(m.UserId)).ToList();
            obj.ForEach(m =>
            {
                m.ConnectionId = friendOnlineDetails.Where(x => x.UserId == m.UserId).Select(x => x.ConnectionId).ToList();
            });
            return obj;
        }
        public UserProfile GetUserById(string userId)
        {
            var obj = _context.UserProfiles.Where(m => m.UserId == userId).FirstOrDefault();
            return obj;
        }
        public string[] GetFriendUserIds(string userId)
        {
            var arr = _context.FriendMappings.Where(m => (m.UserId == userId || m.EndUserId == userId) && m.RequestStatus == "Accepted" && m.IsActive == true).Select(m => m.UserId == userId ? m.EndUserId : m.UserId).ToArray();
            return arr;
        }
        public List<FriendRequestResultModel> GetSentFriendRequests(string userId)
        {
            var list = (from u in _context.FriendMappings
                        join v in _context.UserProfiles on u.EndUserId equals v.UserId
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
            var list = (from u in _context.FriendMappings
                        join v in _context.UserProfiles on u.UserId equals v.UserId
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
            var list = (from u in _context.FriendMappings
                        join v in _context.UserProfiles on u.EndUserId equals v.UserId
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
            var objList = _context.UserProfiles.Where(m => m.FirstName.ToLower().Contains(name.ToLower()) && m.UserId != userId && !friendIds.Contains(m.UserId)).ToList();
            var receivedRequests = GetReceivedFriendRequests(userId);
            var sentRequests = GetSentFriendRequests(userId);
            List<UserSearchResultModel> list = new List<UserSearchResultModel>();
            foreach (var item in objList)
            {
                bool isReceived = false;
                var receivedRequest = receivedRequests.Where(x => x.UserInfo.UserId == item.UserId).FirstOrDefault();
                if (receivedRequest != null)
                {
                    isReceived = true;
                }
                var userInfo = new UserSearchResultModel();
                userInfo.IsRequestReceived = isReceived;
                userInfo.UserInfo = item;
                var sentRequest = sentRequests.Where(m => m.UserInfo.UserId == item.UserId).FirstOrDefault();
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
            FriendMapping objentity = new FriendMapping();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.EndUserId = endUserId;
            objentity.IsActive = true;
            objentity.UserId = loggedInUserId;
            objentity.RequestStatus = "Sent";
            objentity.UpdatedOn = System.DateTime.Now;
            _context.FriendMappings.Add(objentity);
            _context.SaveChanges();
        }
        public UserNotification SaveUserNotification(string notificationType, string fromUserId, string toUserId)
        {
            UserNotification notification = new UserNotification();
            notification.CreatedOn = System.DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserId = fromUserId;
            notification.Status = "New";
            notification.UpdatedOn = System.DateTime.Now;
            notification.ToUserId = toUserId;
            _context.UserNotifications.Add(notification);
            _context.SaveChanges();
            return notification;
        }
        public FriendMapping GetFriendRequestStatus(string userId)
        {
            var obj = _context.FriendMappings.Where(m => (m.EndUserId == userId || m.UserId == userId) && m.IsActive == true).FirstOrDefault();
            return obj;
        }
        public int ResponseToFriendRequest(string requestorId, string requestResponse, string endUserId)
        {
            var request = _context.FriendMappings.Where(m => m.EndUserId == endUserId && m.UserId == requestorId && m.IsActive == true).FirstOrDefault();
            if (request != null)
            {
                request.RequestStatus = requestResponse;
                request.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
            }
            var notification = _context.UserNotifications.Where(m => m.ToUserId == endUserId && m.FromUserId == requestorId && m.IsActive == true && m.NotificationType == "FriendRequest").FirstOrDefault();
            if (notification != null)
            {
                notification.IsActive = false;
                notification.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
                return notification.NotificationId;
            }
            return 0;
        }
        public List<UserNotificationListResultModel> GetUserNotifications(string toUserId)
        {
            var listQuery = (from u in _context.UserNotifications
                             join v in _context.UserProfiles on u.FromUserId equals v.UserId
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
            var list = listQuery.Take(3).ToList();
            list.ForEach(m => m.TotalNotifications = totalNotifications);
            return list;
        }
        public int GetUserNotificationCounts(string toUserId)
        {
            int count = _context.UserNotifications.Where(m => m.Status == "New" && m.ToUserId == toUserId && m.IsActive == true).Count();
            return count;
        }
        public void ChangeNotificationStatus(int[] notificationIds)
        {
            _context.UserNotifications.Where(m => notificationIds.Contains(m.NotificationId)).ToList().ForEach(m => m.Status = "Read");
            _context.SaveChanges();
        }
        public FriendMapping RemoveFriendMapping(int friendMappingId)
        {
            var obj = _context.FriendMappings.Where(m => m.FriendMappingId == friendMappingId).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
                _context.SaveChanges();
            }
            return obj;
        }
        public void UpdateProfilePicture(string userId, string profilePicturePath)
        {

        }
        public List<UserProfile> GetUsersByLinqQuery(Expression<Func<UserProfile, bool>> where)
        {
            var objList = _context.UserProfiles.Where(where).ToList();
            return objList;
        }
        public List<OnlineUserDetailViewModel> GetRecentChats(string currentUserId)
        {
            string[] friends = GetFriendUserIds(currentUserId);
            var recentMessages = _context.ChatMessages.Where(m => m.IsActive == true && (m.ToUserId == currentUserId || m.FromUserId == currentUserId)).OrderByDescending(m => m.CreatedOn).ToList();
            var userIds = recentMessages.Select(m => (m.ToUserId == currentUserId ? m.FromUserId : m.ToUserId)).Distinct().ToArray();
            var userIdsList = userIds.ToList();
            var messagesByUserId = recentMessages.Where(m => m.ToUserId == currentUserId && m.Status == "Sent").ToList();
            var newMessagesCount = (from p in messagesByUserId
                                    group p by p.FromUserId into g
                                    select new { FromUserId = g.Key, Count = g.Count() }).ToList();
            var onlineUserIds = _context.OnlineUsers.Where(m => friends.Contains(m.UserId) && userIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserId).ToArray();
            var users = (from m in _context.UserProfiles
                         join v in userIdsList on m.UserId equals v
                         select new OnlineUserDetailViewModel
                         {
                             UserId = m.UserId,
                             Name = m.FirstName,
                             ProfilePicture = m.ImageUrl,
                             Gender = m.Gender,
                             IsOnline = onlineUserIds.Contains(m.UserId) ? true : false
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
            OnlineUserDetailViewModel obj = new OnlineUserDetailViewModel();
            obj.UserId = userId;
            var objList = _context.OnlineUsers.Where(m => m.UserId == userId && m.IsActive == true).ToList();
            if (objList != null && objList.Count > 0)
            {
                obj.IsOnline = false;
                var onlineConnections = objList.Where(m => m.IsOnline).ToList();
                var offlineConnections = objList.Where(m => !m.IsOnline).ToList();
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
            var obj = _context.UserProfiles.Where(m => m.UserId == userId).FirstOrDefault();
            if (obj != null)
            {
                obj.ImageUrl = imagePath;
                obj.UpdatedOn = System.DateTime.Now;
                _context.SaveChanges();
                SaveUserImage(userId, imagePath, true);
            }
        }
        public void SaveUserImage(string userId, string imagePath, bool isProfilePicture)
        {
            if (isProfilePicture)
            {
                var existingProfilePicture = _context.UserImages.Where(m => m.UserId == userId && m.IsActive == true && m.IsProfilePicture == true).FirstOrDefault();
                if (existingProfilePicture != null)
                {
                    existingProfilePicture.IsProfilePicture = false;
                    _context.SaveChanges();
                }
            }
            UserImage objentity = new UserImage();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.ImagePath = imagePath;
            objentity.IsActive = true;
            objentity.IsProfilePicture = isProfilePicture;
            objentity.UserId = userId;
            _context.UserImages.Add(objentity);
            _context.SaveChanges();
        }
        public List<OnlineUserDetailViewModel> GetFriends(string userId)
        {
            var friendIds = GetFriendUserIds(userId);
            var onlineUserIds = _context.OnlineUsers.Where(m => friendIds.Contains(m.UserId) && m.IsActive == true && m.IsOnline == true).Select(m => m.UserId).ToArray();
            var users = _context.UserProfiles.Where(m => friendIds.Contains(m.UserId)).Select(m => new OnlineUserDetailViewModel
            {
                UserId = m.UserId,
                Name = m.FirstName,
                ProfilePicture = m.ImageUrl,
                Gender = m.Gender,
                IsOnline = onlineUserIds.Contains(m.UserId) ? true : false
            }).ToList();
            return users;
        }
        public UserProfile GetByUserId(string UserId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.UserProfiles.FirstOrDefault(x => x.UserId == UserId);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
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
            using (var db = new ApplicationDbContext())
            {

                var user = db.UserProfiles.FirstOrDefault(x => x.UserId == profile.UserId);

                if (user == null)
                {
                    user = new UserProfile()
                    {
                        UserId = profile.UserId,
                        FirstName = profile.FirstName,
                        Designation = profile.Designation
                    };

                    db.UserProfiles.Add(user);
                }

                db.SaveChanges();
            }
        }
        //public List<UserProfile> GetAllUsers()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        return db.UserProfiles.ToList();

        //    }
        //}
        public async Task CreateUserProfileAsync(string userId, string userName,string userImage="")
        {
            using (var db = new ApplicationDbContext())
            {
                db.UserProfiles.Add(new UserProfile()
                {
                    UserId = userId,
                    FirstName = userName,
                    ImageUrl = !string.IsNullOrEmpty(userImage) ? userImage : @"/images/DefaultPhoto.png" ,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    IsActive = true
                });
                await db.SaveChangesAsync();
            }
        }
        public List<UserSearchResultModel> SerachUser(string userName)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.UserProfiles
                    .Where(x => x.FirstName.Contains(userName))
                    .Select(x=> new UserSearchResultModel { UserInfo = x})
                    .ToList();
            }
        }

    }
}