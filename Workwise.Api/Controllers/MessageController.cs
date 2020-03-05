using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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
        public IHttpActionResult SaveChatMessage(ChatMessage objentity)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _messageService.SaveChatMessage(objentity);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(MessageRecordResultModel))]
        public IHttpActionResult GetChatMessagesByUserId(string currentUserId, string toUserId, int lastMessageId = 0)
        {
            try
            {
                var result = _messageService.GetChatMessagesByUserId(currentUserId, toUserId, lastMessageId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException)
            {

                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateMessageStatusByUserId(UpdateMessageStatusRequest model)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _messageService.UpdateMessageStatusByUserId(model.FromUserId, model.CurrentUserId);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult UpdateMessageStatusByMessageId(int messageId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _messageService.UpdateMessageStatusByMessageId(messageId);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();
        }
    }
}
