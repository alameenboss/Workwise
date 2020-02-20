using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Service.Interface;

namespace Workwise.Controllers
{

    [Authorize]
    public class MyProfileFeedController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;


        public MyProfileFeedController(ApplicationUserManager userManager)
        {
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public MyProfileFeedController(IUserService userProfileRepo, IPostService postService)
        {
            _userService = userProfileRepo;
            _postService = postService;
        }

        public ActionResult Index(string id)
        {
            var myuserId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id)) id = myuserId;
            var model = _userService.GetUserById(id);
            model.Posts = _postService.GetLatestPostByUser(id).ToList();
            model.Following = _userService.FollowingList(id, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userService.FollowersList(id, myuserId).Select(x => x.UserInfo).ToList();

            

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
            _userService.SaveProfile(profile);

            return RedirectToAction("Index");
        }

    }
}