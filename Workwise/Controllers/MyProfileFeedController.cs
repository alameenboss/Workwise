using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Workwise.Helper;

namespace Workwise.Controllers
{
    [Authorize]
    public class MyProfileFeedController : Controller
    {
        public ActionResult Index(string mode)
        {
            ViewData["Mode"] = mode;
            ViewData["ImageUrl"] = SessionHelper.GetUser(User.Identity.GetUserId()).ImageUrl;
            return View();
        }

        public ActionResult LoginPartial()
        {
            var model = SessionHelper.GetUser(User.Identity.GetUserId());
            return PartialView("_LoginPartial", model);
        }



        
    }
}