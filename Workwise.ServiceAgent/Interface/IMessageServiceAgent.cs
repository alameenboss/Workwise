﻿using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent.Interface
{
    public interface IMessageServiceAgent
    {
        Task<ChatMessageViewModel> SaveChatMessage(ChatMessageViewModel objentity);
        MessageRecordViewModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0);
        void UpdateMessageStatusByUserId(string fromUserId, string currentUserId);
        void UpdateMessageStatusByMessageId(int messageId);
    }
}
