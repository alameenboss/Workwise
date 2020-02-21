using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workwise.ViewModel
{
    public class ChatMessageViewModel
    {
        public int ChatMessageId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime ViewedOn { get; set; }
        public bool IsActive { get; set; }

        public UserViewModel UserDetail { get; set; }
        public List<MessageViewModel> ChatMessages { get; set; }
        public bool IsOnline { get; set; }
        public string LastSeen { get; set; }
        public int LastChatMessageId { get; set; }
    }
}
