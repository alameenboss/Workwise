using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class MessageServiceAgent : IMessageServiceAgent
    {
        
        public ChatMessageViewModel SaveChatMessage(ChatMessageViewModel objentity)
        {
            return new ChatMessageViewModel();
        }
        public MessageRecordViewModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return new MessageRecordViewModel();
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
           
        }
    }

}