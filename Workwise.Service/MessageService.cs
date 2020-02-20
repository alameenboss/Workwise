using System.Linq;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.ResultModel;
using Workwise.Service.Interface;

namespace Workwise.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            return _messageRepository.SaveChatMessage(objentity);
        }
        public MessageRecordResultModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return _messageRepository.GetChatMessagesByUserId(currentUserId,toUserId,lastMessageId);
        }
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            _messageRepository.UpdateMessageStatusByUserId(fromUserId, currentUserId);
        }
        public void UpdateMessageStatusByMessageId(int messageId)
        {
            _messageRepository.UpdateMessageStatusByMessageId(messageId);
        }
    }

}