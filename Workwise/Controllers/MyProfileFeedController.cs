using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Helper;
using Workwise.Models;

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
            var model = new List<Post>();
            if (string.IsNullOrEmpty(id)) id = User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            if (user != null)
            {
                model = postrepository.GetLatestPostByUser(user.Id).ToList();
                ViewData["username"] = user.UserName;
                var userprofile = new UserProfileRepository();
                var _user = userprofile.GetByUserId(user.Id);
                ViewData["userimage"] = string.IsNullOrEmpty(_user?.ImageUrl) ? @"\images\DefaultPhoto.png" : _user?.ImageUrl;
                ViewData["firstname"] = string.IsNullOrEmpty(_user?.FirstName) ? "FirstName" : _user?.FirstName;
            }
            
            
            
            return View(model);
        }

        public ActionResult LoginPartial()
        {
            var model = SessionHelper.GetUser(User.Identity.GetUserId());
            return PartialView("_LoginPartial", model);
        }

        public ActionResult SaveUserInfo(UserProfile profile)
        {
            var userprofilerepo = new UserProfileRepository();
            var user = userprofilerepo.GetByUserId(User.Identity.GetUserId());
            if (user == null)
                user = new UserProfile(); 

            user.FirstName = profile.FirstName;
            user.Designation = profile.Designation;
            userprofilerepo.SaveProfile(user);

            return RedirectToAction("Index");
        }

        
    }
}