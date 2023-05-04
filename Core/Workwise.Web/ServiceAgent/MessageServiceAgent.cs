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

        public ChatMessageViewModel SaveChatMessage(ChatMessageViewModel objentity)
        {
            return _httpClient.PostDataAsync(Constent.Message.SaveChatMessage, objentity);
        }
        public MessageRecordViewModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return _httpClient.GetAsync<MessageRecordViewModel>(string.Format(Constent.Message.GetChatMessagesByUserId,currentUserId,toUserId,lastMessageId));
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            var model = new UpdateMessageStatusViewModel()
            {
                FromUserId = fromUserId,
                CurrentUserId = currentUserId
            };
            _httpClient.PostDataAsync(Constent.Message.UpdateMessageStatusByUserId, model);
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
            _httpClient.PostDataAsync(Constent.Message.UpdateMessageStatusByMessageId, messageId);
        }
    }

}