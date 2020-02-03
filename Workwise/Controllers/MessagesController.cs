using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using Workwise.Data.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IUserProfileRepository _userProfileRepo;
        
        public MessagesController(IUserProfileRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
           
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var model = _userProfileRepo.GetAllUsers().Where(x => x.UserId != User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Messages\_UserPartial.cshtml", model);
        }
    }
}