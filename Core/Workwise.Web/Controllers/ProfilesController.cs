using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;
using Workwise.Web.Helper;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly IUserServiceAgent _userServiceAgent;
       
       
        public ProfilesController(IUserServiceAgent userProfileRepo)
        {
            _userServiceAgent = userProfileRepo;
        }
        public ActionResult Index()
        {
            var model = _userServiceAgent.GetAllUsers(100, "alameen02111988");
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? "alameen02111988" : id;
            var model = _userServiceAgent.FollowingList(userid, "alameen02111988");
            var userModel = _userServiceAgent.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text = userModel.FirstName + " is following " + model.Count + " person.";

            return View("Index", model);
        }
        public ActionResult Followers(string id)
        {
            var userid = string.IsNullOrEmpty(id) ? "alameen02111988" : id;
            var model = _userServiceAgent.FollowersList(userid, "alameen02111988");
            var userModel = _userServiceAgent.GetByUserId(userid);
            ViewBag.Image = userModel.ImageUrl;
            ViewBag.Text = userModel.FirstName + " has " + model.Count + " followers.";
            return View("Index", model);
        }
        public ActionResult Randomuser(int id)
        {
            var model = _userServiceAgent.GetManyDummyUser(1, id);
            return View(model);
        }


    }
}