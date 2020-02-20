using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;
using Workwise.ResultModel;

namespace Workwise.Data.Interface
{
    public interface IMessageRepository
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        MessageRecordResultModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0);
        void UpdateMessageStatusByUserId(string fromUserId, string currentUserId);
        void UpdateMessageStatusByMessageId(int messageId);
    }
}
