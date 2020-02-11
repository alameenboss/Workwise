using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Helper;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IUserRepository _userProfileRepo;      
        private readonly IMessage _MessageRepo;
        public MessagesController(IUserRepository userProfileRepo, IMessage MessageRepo)
        {
            _userProfileRepo = userProfileRepo;
            _MessageRepo = MessageRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var model = _userProfileRepo.GetAllUsers(10, User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Messages\_UserPartial.cshtml", model);
        }

        public ActionResult _Messages(string Id)
        {
            var userModel = DefaultsHelper.GetUserModel(Id);
            var messages = _MessageRepo.GetChatMessagesByUserId(User.Identity.GetUserId(), Id);
            var objmodel = new ChatMessageViewModel();
            objmodel.UserDetail = userModel;
            objmodel.ChatMessages = messages.Messages.Select(m => DefaultsHelper.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            var onlineStatus = _userProfileRepo.GetUserOnlineStatus(Id);
            if (onlineStatus != null)
            {
                objmodel.IsOnline = onlineStatus.IsOnline;
                objmodel.LastSeen = Convert.ToString(onlineStatus.LastUpdationTime);
            }
            return PartialView(objmodel);
        }
        public ActionResult GetRecentMessages(string Id, int lastChatMessageId)
        {
            var messages = _MessageRepo.GetChatMessagesByUserId(User.Identity.GetUserId(), Id, lastChatMessageId);
            var objmodel = new ChatMessageViewModel();
            objmodel.ChatMessages = messages.Messages.Select(m => DefaultsHelper.GetMessageModel(m)).ToList();
            objmodel.LastChatMessageId = messages.LastChatMessageId;
            return Json(objmodel, JsonRequestBehavior.AllowGet);
        }

    }
}