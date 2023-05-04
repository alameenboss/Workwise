using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;
using Workwise.Web.Helper;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IUserServiceAgent _userServiceAgent;      
        private readonly IMessageServiceAgent _MessageServiceAgent;
        private readonly IDefaultsHelper _defaultHelper;
        public MessagesController(IUserServiceAgent userServiceAgent, IMessageServiceAgent MessageServiceAgent, IDefaultsHelper defaultHelper)
        {
            _userServiceAgent = userServiceAgent;
            _MessageServiceAgent = MessageServiceAgent;
            _defaultHelper = defaultHelper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {

            var model = _userServiceAgent.MyFriendsList("alameen02111988").ToList();
            return PartialView(@"~\Views\Messages\_UserPartial.cshtml", model);
        }

        public ActionResult _Messages(string Id)
        {
            var userModel = _defaultHelper.GetUserModel(Id);
            var messages = new MessageRecordViewModel();//_MessageServiceAgent.GetChatMessagesByUserId("alameen02111988", Id);
            var objmodel = new ChatMessageViewModel();
            objmodel.UserDetail = userModel;
            objmodel.ChatMessages = messages.Messages.Select(m => _defaultHelper.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            var onlineStatus = _userServiceAgent.GetUserOnlineStatus(Id);
            if (onlineStatus != null)
            {
                objmodel.IsOnline = onlineStatus.IsOnline;
                objmodel.LastSeen = Convert.ToString(onlineStatus.LastUpdationTime);
            }
            return PartialView(objmodel);
        }
        public ActionResult GetRecentMessages(string Id, int lastChatMessageId)
        {
            var messages = new MessageRecordViewModel();//_MessageServiceAgent.GetChatMessagesByUserId("alameen02111988", Id, lastChatMessageId);
            var objmodel = new ChatMessageViewModel();
            objmodel.ChatMessages = messages.Messages.Select(m => _defaultHelper.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            return Ok(objmodel);
        }

    }
}