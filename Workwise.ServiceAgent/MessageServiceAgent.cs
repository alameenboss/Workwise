﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class MessageServiceAgent : IMessageServiceAgent
    {
        private readonly IHttpClient _httpClient;
        public MessageServiceAgent(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatMessageViewModel> SaveChatMessage(ChatMessageViewModel objentity)
        {
            return await _httpClient.PostDataAsync<ChatMessageViewModel>(Constent.Message.SaveChatMessage, objentity);
        }
        public MessageRecordViewModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return _httpClient.Get<MessageRecordViewModel>(string.Format(Constent.Message.GetChatMessagesByUserId,currentUserId,toUserId,lastMessageId));
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            var model = new UpdateMessageStatusViewModel()
            {
                FromUserId = fromUserId,
                CurrentUserId = currentUserId
            };
            _httpClient.PostData(Constent.Message.UpdateMessageStatusByUserId, model);
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
             _httpClient.PostData(Constent.Message.UpdateMessageStatusByMessageId, messageId);

        }
    }

}