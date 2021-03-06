﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Model;
using Workwise.ResultModel;

namespace Workwise.Service.Interface
{
    public interface IMessageService
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        Task<MessageRecordResultModel> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0);
        void UpdateMessageStatusByUserId(string fromUserId, string currentUserId);
        void UpdateMessageStatusByMessageId(int messageId);
    }
}
