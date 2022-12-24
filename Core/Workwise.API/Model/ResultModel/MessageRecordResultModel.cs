using Workwise.API.Models;

namespace Workwise.API.Model.ResultModel
{
    public class MessageRecordResultModel
    {
        public List<ChatMessage>? Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}
