using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Data.Models;

namespace Workwise.Controllers
{

    [Authorize]
    public class MyProfileFeedController : BaseController
    {
        private readonly IUserRepository _userProfileRepo;
        private readonly IPostRepository _postrepository;


        public MyProfileFeedController(ApplicationUserManager userManager)
        {
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public MyProfileFeedController(IUserRepository userProfileRepo, IPostRepository postrepository)
        {
            _userProfileRepo = userProfileRepo;
            _postrepository = postrepository;
        }

        public ActionResult Index(string id)
        {
            var myuserId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id)) id = myuserId;
            var model = _userProfileRepo.GetUserById(id);
            model.Posts = _postrepository.GetLatestPostByUser(id).ToList();
            model.Following = _userProfileRepo.FollowingList(id, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userProfileRepo.FollowersList(id, myuserId).Select(x => x.UserInfo).ToList();

            

            var user = UserManager.FindById(id);
            if (user != null)
            {
                ViewData["username"] = user.UserName;
                ViewData["userimage"] = string.IsNullOrEmpty(model?.ImageUrl) ? @"\images\DefaultPhoto.png" : model?.ImageUrl;
                ViewData["firstname"] = string.IsNullOrEmpty(model?.FirstName) ? "FirstName" : model?.FirstName;
            }
            
            
            
            return View(model);
        }

        public ActionResult SaveUserInfo(UserProfile profile)
        {
            profile.UserId = User.Identity.GetUserId();
            _userProfileRepo.SaveProfile(profile);

            return RedirectToAction("Index");
        }

    }
}