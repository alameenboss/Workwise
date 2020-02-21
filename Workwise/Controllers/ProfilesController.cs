using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Workwise.ServiceAgent.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfilesController : BaseController
    {
        private readonly IUserServiceAgent _userServiceAgent;
        public ProfilesController()
        {
        }
        public ProfilesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ProfilesController(IUserServiceAgent userProfileRepo)
        {
            _userServiceAgent = userProfileRepo;
        }
        public ActionResult Index()
        {
            var model = _userServiceAgent.GetAllUsers(100, User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userServiceAgent.FollowingList(userid, User.Identity.GetUserId());
            var userModel = _userServiceAgent.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text = userModel.FirstName + " is following " + model.Count + " person.";
           
            return View("Index",model);
        }
        public ActionResult Followers(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? User.Identity.GetUserId() : id;
            var model = _userServiceAgent.FollowersList(userid, User.Identity.GetUserId());
            var userModel = _userServiceAgent.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text =  userModel.FirstName + " has " + model.Count + " followers.";
            return View("Index", model);
        }
        public ActionResult Randomuser(int id)
        {
            var model = _userServiceAgent.GetManyDummyUser(1, id);
            return View(model);
        }

        
    }
}