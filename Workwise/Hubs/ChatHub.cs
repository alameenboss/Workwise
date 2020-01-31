using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Workwise.Hubs
{
    public class Users
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public string UserImage { get; set; }

    }

    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void Send(string name, string message,string userImage, string connId,string myConnectionId)
        {
            var msg = $"<div class='main-message-box ta-right'><div class='message-dt'><div class='message-inner-dt'><p>{message}</p></div><span>Sat, Aug 23, 1:08 PM</span></div><div class='messg-usr-img'><img src='{userImage}' alt='' class='mCS_img_loaded'></div></div>";
            Clients.Client(connId).addNewMessageToPage(name, msg, myConnectionId);
        }

        static List<Users> SignalRUsers = new List<Users>();

        public void Connect(string userName,string userImage)
        {
            var id = Context.ConnectionId;

            if (SignalRUsers.Count(x => x.ConnectionId == id) == 0)
            {
                SignalRUsers.Add(new Users { ConnectionId = id, UserName = userName, UserImage = userImage });
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = SignalRUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                SignalRUsers.Remove(item);
            }

            return base.OnDisconnected(stopCalled);
        }

        public List<Users> GetAllActiveConnections()
        {
            return SignalRUsers.ToList();
        }

    }
}