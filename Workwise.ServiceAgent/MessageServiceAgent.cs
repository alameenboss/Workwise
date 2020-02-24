using System.Collections.Generic;
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

        public ChatMessageViewModel SaveChatMessage(ChatMessageViewModel objentity)
        {
            return _httpClient.PostData<ChatMessageViewModel>(Constent.Message.SaveChatMessage, objentity);
        }
        public MessageRecordViewModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return _httpClient.Get<MessageRecordViewModel>(string.Format(Constent.Message.GetChatMessagesByUserId,currentUserId,toUserId,lastMessageId));
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            _httpClient.PostData<ChatMessageViewModel,string,string>(Constent.Message.UpdateMessageStatusByUserId, fromUserId, currentUserId);
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
             _httpClient.PostData<ChatMessageViewModel,int>(Constent.Message.UpdateMessageStatusByMessageId, messageId);

        }
    }

}