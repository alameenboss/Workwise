using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workwise.ViewModel
{

    public class ChatMessageViewModel
    {
        public UserViewModel UserDetail { get; set; }
        public List<MessageViewModel> ChatMessages { get; set; }
        public bool IsOnline { get; set; }
        public string LastSeen { get; set; }
        public int LastChatMessageId { get; set; }
    }
    public class MessageViewModel
    {
        public int ChatMessageId { get; set; }
        public string FromUserId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string ReceivedOn { get; set; }
        public string ViewedOn { get; set; }
        public bool IsActive { get; set; }
    }
    public class MesageBlockViewModel
    {
        public string MessageAlign { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}