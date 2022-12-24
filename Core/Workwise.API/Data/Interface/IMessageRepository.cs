using Workwise.API.Model.ResultModel;
using Workwise.API.Models;

namespace Workwise.API.Data.Interface
{
    public interface IMessageRepository
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        Task<MessageRecordResultModel> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0);
        void UpdateMessageStatusByUserId(string fromUserId, string currentUserId);
        void UpdateMessageStatusByMessageId(int messageId);
    }
}
