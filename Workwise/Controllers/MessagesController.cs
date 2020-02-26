using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
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

            var model = _userServiceAgent.MyFriendsList(User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Messages\_UserPartial.cshtml", model);
        }

        public ActionResult _Messages(string Id)
        {
            var userModel = _defaultHelper.GetUserModel(Id);
            var messages = _MessageServiceAgent.GetChatMessagesByUserId(User.Identity.GetUserId(), Id);
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
            var messages = _MessageServiceAgent.GetChatMessagesByUserId(User.Identity.GetUserId(), Id, lastChatMessageId);
            var objmodel = new ChatMessageViewModel();
            objmodel.ChatMessages = messages.Messages.Select(m => _defaultHelper.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            return Json(objmodel, JsonRequestBehavior.AllowGet);
        }

    }
}