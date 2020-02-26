using System.Web.Http;
using Workwise.Model;
using Workwise.ResultModel;
using Workwise.Service.Interface;

namespace Workwise.Api.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService = null;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public ChatMessage SaveChatMessage(ChatMessage objentity)
        {
            return _messageService.SaveChatMessage(objentity);
        }

        public MessageRecordResultModel GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            return _messageService.GetChatMessagesByUserId(currentUserId, toUserId, lastMessageId);
        }

        [HttpPost]
        public void UpdateMessageStatusByUserId(string fromUserId, string currentUserId)
        {
            _messageService.UpdateMessageStatusByUserId(fromUserId, currentUserId);
        }

        [HttpPost]
        public void UpdateMessageStatusByMessageId(int messageId)
        {
            _messageService.UpdateMessageStatusByMessageId(messageId);
        }
    }
}
