using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Hubs
{
    public class Users
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public string UserImage { get; set; }

    }

    //[HubName("chat")]
    public class ChatHub : Hub
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IMessageServiceAgent _messageServiceAgent;
        public ChatHub(IUserServiceAgent userServiceAgent, IMessageServiceAgent messageServiceAgent)
        {
            _userServiceAgent = userServiceAgent;
            _messageServiceAgent = messageServiceAgent;
        }

        public override Task OnConnected()
        {
            var userId = Context.QueryString["UserId"];
            if (userId != null)
            {
                string uId = userId;
                _userServiceAgent.SaveUserOnlineStatus(new OnlineUserViewModel { UserId = uId, ConnectionId = Context.ConnectionId, IsOnline = true });
                RefreshOnlineUsers(uId);
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.QueryString["UserId"];
            if (userId != null)
            {
                string uId = userId;
                _userServiceAgent.SaveUserOnlineStatus(new OnlineUserViewModel { UserId = uId, ConnectionId = Context.ConnectionId, IsOnline = false });
                RefreshOnlineUsers(uId);
            }
            return base.OnDisconnected(stopCalled);
        }
        public List<string> GetActiveConnectionIds(List<string> connectionIds)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var connections = heartBeat.GetConnections();
            if (connections != null && connectionIds != null)
            {
                var filterdConnectionIds = connections.Where(m => connectionIds.Contains(m.ConnectionId)).Select(m => m.ConnectionId).ToList();
                return filterdConnectionIds;
            }
            return connectionIds;
        }
        public void RefreshOnlineUsers(string userId)
        {
            var users = _userServiceAgent.GetOnlineFriends(userId);
            RefreshOnlineUsersByConnectionIds(users.SelectMany(m => m.ConnectionId).ToList(), userId);
        }
        public void RefreshOnlineUsersByConnectionIds(List<string> connectionIds, string userId = "")
        {
            Clients.Clients(connectionIds).RefreshOnlineUsers();
            if (!string.IsNullOrEmpty(userId))
            {
                var onlineStatus = _userServiceAgent.GetUserOnlineStatus(userId);
                if (onlineStatus != null)
                {
                    Clients.Clients(connectionIds).RefreshOnlineUserByUserId(userId, onlineStatus.IsOnline, Convert.ToString(onlineStatus.LastUpdationTime));
                }
            }
        }
        public void SendRequest(string userId, string loggedInUserId)
        {
            _userServiceAgent.SendFriendRequest(userId, loggedInUserId);
            SendNotification(loggedInUserId, userId, "FriendRequest");
        }
        public void SendNotification(string fromUserId, string toUserId, string notificationType)
        {
            int notificationId = _userServiceAgent.SaveUserNotification(notificationType, fromUserId, toUserId);
            var connectionId = _userServiceAgent.GetUserConnectionId(toUserId);
            if (connectionId != null && connectionId.Count() > 0)
            {
                var userInfo = DefaultsHelper.GetUserModel(fromUserId);
                int notificationCounts = _userServiceAgent.GetUserNotificationCounts(toUserId);
                Clients.Clients(connectionId).ReceiveNotification(notificationType, userInfo, notificationId, notificationCounts);
            }
        }
        public void SendResponseToRequest(string requestorId, string requestResponse, string endUserId)
        {
            var notificationId = _userServiceAgent.ResponseToFriendRequest(requestorId, requestResponse, endUserId);
            if (notificationId > 0)
            {
                var connectionId = _userServiceAgent.GetUserConnectionId(endUserId);
                if (connectionId != null && connectionId.Count() > 0)
                {
                    Clients.Clients(connectionId).RemoveNotification(notificationId);
                }
            }
            if (requestResponse == "Accepted")
            {
                SendNotification(endUserId, requestorId, "FriendRequestAccepted");
                List<string> connectionIds = _userServiceAgent.GetUserConnectionId(new string[] { endUserId, requestorId });
                RefreshOnlineUsersByConnectionIds(connectionIds);
            }
        }
        public void RefreshNotificationCounts(string toUserId)
        {
            var connectionId = _userServiceAgent.GetUserConnectionId(toUserId);
            if (connectionId != null && connectionId.Count() > 0)
            {
                int notificationCounts = _userServiceAgent.GetUserNotificationCounts(toUserId);
                Clients.Clients(connectionId).RefreshNotificationCounts(notificationCounts);
            }
        }
        public void ChangeNotitficationStatus(string notificationIds, string toUserId)
        {
            if (!string.IsNullOrEmpty(notificationIds))
            {
                string[] arrNotificationIds = notificationIds.Split(',');
                int[] ids = arrNotificationIds.Select(m => Convert.ToInt32(m)).ToArray();
                _userServiceAgent.ChangeNotificationStatus(ids);
                RefreshNotificationCounts(toUserId);
            }
        }
        public void UnfriendUser(int friendMappingId)
        {
            var friendMapping = _userServiceAgent.RemoveFriendMapping(friendMappingId);
            if (friendMapping != null)
            {
                List<string> connectionIds = _userServiceAgent.GetUserConnectionId(new string[] { friendMapping.EndUserId, friendMapping.UserId });
                RefreshOnlineUsersByConnectionIds(connectionIds);
            }
        }
        public void SendMessage(string fromUserId, string toUserId, string message, string fromUserName, string fromUserProfilePic, string toUserName, string toUserProfilePic)
        {
            ChatMessageViewModel objentity = new ChatMessageViewModel();
            objentity.CreatedOn = System.DateTime.Now;
            objentity.FromUserId = fromUserId;
            objentity.IsActive = true;
            objentity.Message = message;
            objentity.ViewedOn = System.DateTime.Now;
            objentity.Status = "Sent";
            objentity.ToUserId = toUserId;
            objentity.UpdatedOn = System.DateTime.Now;
            var obj = _messageServiceAgent.SaveChatMessage(objentity);
            var messageRow = DefaultsHelper.GetMessageModel(obj);
            List<string> connectionIds = _userServiceAgent.GetUserConnectionId(new string[] { fromUserId, toUserId });
            Clients.Clients(connectionIds).AddNewChatMessage(messageRow, fromUserId, toUserId, fromUserName, fromUserProfilePic, toUserName, toUserProfilePic);
        }
        public void SendUserTypingStatus(string toUserId, string fromUserId)
        {
            List<string> connectionIds = _userServiceAgent.GetUserConnectionId(new string[] { toUserId });
            if (connectionIds.Count > 0)
            {
                Clients.Clients(connectionIds).UserIsTyping(fromUserId);
            }
        }
        public void UpdateMessageStatus(int messageId, string currentUserId, string fromUserId)
        {
            if (messageId > 0)
            {
                _messageServiceAgent.UpdateMessageStatusByMessageId(messageId);
            }
            else
            {
                _messageServiceAgent.UpdateMessageStatusByUserId(fromUserId, currentUserId);
            }
            List<string> connectionIds = _userServiceAgent.GetUserConnectionId(new string[] { currentUserId, fromUserId });
            Clients.Clients(connectionIds).UpdateMessageStatusInChatWindow(messageId, currentUserId, fromUserId);
        }
    }

}