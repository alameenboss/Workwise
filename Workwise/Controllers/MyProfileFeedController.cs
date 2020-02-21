using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{

    [Authorize]
    public class MyProfileFeedController : BaseController
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IPostServiceAgent _postServiceAgent;


        public MyProfileFeedController(ApplicationUserManager userManager)
        {
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public MyProfileFeedController(IUserServiceAgent userProfileRepo, IPostServiceAgent postService)
        {
            _userServiceAgent = userProfileRepo;
            _postServiceAgent = postService;
        }

        public ActionResult Index(string id)
        {
            var myuserId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id)) id = myuserId;
            var model = _userServiceAgent.GetUserById(id);
            model.Posts = _postServiceAgent.GetLatestPostByUser(id).ToList();
            model.Following = _userServiceAgent.FollowingList(id, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userServiceAgent.FollowersList(id, myuserId).Select(x => x.UserInfo).ToList();

            

            var user = UserManager.FindById(id);
            if (user != null)
            {
                ViewData["username"] = user.UserName;
                ViewData["userimage"] = string.IsNullOrEmpty(model?.ImageUrl) ? @"\images\DefaultPhoto.png" : model?.ImageUrl;
                ViewData["firstname"] = string.IsNullOrEmpty(model?.FirstName) ? "FirstName" : model?.FirstName;
            }
            
            
            
            return View(model);
        }

        public ActionResult SaveUserInfo(UserProfileViewModel profile)
        {
            profile.UserId = User.Identity.GetUserId();
            _userServiceAgent.SaveProfile(profile);

            return RedirectToAction("Index");
        }

    }
}