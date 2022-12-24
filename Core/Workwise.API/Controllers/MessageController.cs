using Microsoft.AspNetCore.Mvc;
using Workwise.API.Models;
using Workwise.API.Models;
using Workwise.API.Service.Interface;
using Workwise.API.Model.ResultModel;
using Workwise.API.Model.RequestModel;

namespace Workwise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost("SaveChatMessage")]
        public IActionResult SaveChatMessage(ChatMessage objentity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            ChatMessage result = messageService.SaveChatMessage(objentity);

            return Ok(result);
        }

        [HttpGet("GetChatMessagesByUserId")]
        public async Task<IActionResult> GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {

            MessageRecordResultModel result = await messageService.GetChatMessagesByUserId(currentUserId, toUserId, lastMessageId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("UpdateMessageStatusByUserId")]
        public IActionResult UpdateMessageStatusByUserId(UpdateMessageStatusRequest model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            messageService.UpdateMessageStatusByUserId(model.FromUserId, model.CurrentUserId);

            return Ok();
        }

        [HttpPost("UpdateMessageStatusByMessageId")]
        public IActionResult UpdateMessageStatusByMessageId(int messageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            messageService.UpdateMessageStatusByMessageId(messageId);

            return Ok();
        }
    }
}
