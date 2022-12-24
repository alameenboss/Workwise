using Workwise.API.Data.Interface;
using Workwise.API.Model.ResultModel;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Service
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
        public async Task<MessageRecordResultModel> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return await _messageRepository.GetChatMessagesByUserId(currentUserId, toUserId, lastMessageId);
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