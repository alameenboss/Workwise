using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Helper;
namespace Workwise.Controllers
{
    [Authorize]
    public class MyProfileFeedController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly PostRepository postrepository;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public MyProfileFeedController()
        {
            postrepository = new PostRepository();
        }
       
        public MyProfileFeedController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Index(string id)
        {
            var user = UserManager.FindByName(id);
            var model = postrepository.GetLatestPostByUser(user.Id);
            ViewData["username"] = id;
            var userprofile = new UserProfileRepository();

            ViewData["userimage"] = userprofile.GetByUserId(user.Id).ImageUrl;
            ViewData["firstname"] = userprofile.GetByUserId(user.Id).FirstName;
            return View(model);
        }

        public ActionResult LoginPartial()
        {
            var model = SessionHelper.GetUser(User.Identity.GetUserId());
            return PartialView("_LoginPartial", model);
        }



        
    }
}