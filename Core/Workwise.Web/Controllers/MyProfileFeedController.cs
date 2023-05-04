using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;
using Workwise.Web.Helper;
using static Workwise.ServiceAgent.Constent;

namespace Workwise.Web.Controllers
{

    [Authorize]
    public class MyProfileFeedController : Controller
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IPostServiceAgent _postServiceAgent;


        public MyProfileFeedController()
        {
            
        }

        public MyProfileFeedController(IUserServiceAgent userProfileRepo, IPostServiceAgent postService)
        {
            _userServiceAgent = userProfileRepo;
            _postServiceAgent = postService;
        }

        public ActionResult Index(string id)
        {
            var myuserId = "alameen02111988";
            if (string.IsNullOrEmpty(id)) id = myuserId;
            var model = _userServiceAgent.GetUserById(id);
            model.Posts = _postServiceAgent.GetLatestPostByUser(id).ToList();
            model.Following = _userServiceAgent.FollowingList(id, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userServiceAgent.FollowersList(id, myuserId).Select(x => x.UserInfo).ToList();



            ViewData["username"] = User?.Identity?.Name?.ToString();
            ViewData["userimage"] = string.IsNullOrEmpty(model?.ImageUrl) ? @"\images\DefaultPhoto.png" : model?.ImageUrl;
            ViewData["firstname"] = string.IsNullOrEmpty(model?.FirstName) ? "FirstName" : model?.FirstName;



            return View(model);
        }

        public ActionResult SaveUserInfo(UserProfileViewModel profile)
        {
            profile.UserId = "alameen02111988";
            _userServiceAgent.SaveProfile(profile);
            return RedirectToAction("Index");
        }
    }
}