using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Data.Models;

namespace Workwise.Data.Interface
{
    public interface IMessage
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        MessageRecords GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0);
        void UpdateMessageStatusByUserId(string fromUserId, string currentUserId);
        void UpdateMessageStatusByMessageId(int messageId);
    }
}
