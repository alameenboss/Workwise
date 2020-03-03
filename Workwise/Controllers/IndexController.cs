using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class IndexController : BaseController
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IPostServiceAgent _postServiceAgent;
        private readonly IDefaultsHelper _defaultHelper;
        public IndexController(IUserServiceAgent userService, IPostServiceAgent postService,IDefaultsHelper defaultHelper)
        {
            _userServiceAgent = userService;
            _postServiceAgent = postService;
            _defaultHelper = defaultHelper;
        }

        public ActionResult Index()
        {
            var myuserId = User.Identity.GetUserId();

            var model = _userServiceAgent.GetUserById(myuserId);
            model.Posts = _postServiceAgent.GetLatestPostByUser(myuserId).ToList();
            model.Following = _userServiceAgent.FollowingList(myuserId, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userServiceAgent.FollowersList(myuserId, myuserId).Select(x => x.UserInfo).ToList();

            return View(model);
        }

        public ActionResult GetPosts()
        {
            var text = new RandomText();
            text.AddContentParagraphs(RandomGenerator.GenerateLockedRandom(1, 10),
                1, 
                RandomGenerator.GenerateLockedRandom(10, 20),
                RandomGenerator.GenerateLockedRandom(1, 10), 
                RandomGenerator.GenerateLockedRandom(10, 30));
             
            var model = new PostViewModel()
            {
                PostedOn = DateTime.Now.AddSeconds(-90),
                PostedBy = _defaultHelper.GetUser(User.Identity.GetUserId()),
                Title = new RandomText().GetNewSentence(5),
                Description = text.Content,
                Rate = RandomGenerator.GenerateLockedRandom(20, 400),
                PostImages = new List<ImageViewModel>()
                {
                    new ImageViewModel()
                    {
                        ImageUrl = $"/images/postimages/image{RandomGenerator.GenerateLockedRandom(1, 14)}.jpg"
                    }
                }
            };
            return PartialView(@"~\Views\Index\_PostDetail.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(PostViewModel model, HttpPostedFileBase PostImage)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }
           
            try
            {
                if (PostImage != null)
                {
                    model.PostImages.Add(new ImageViewModel()
                    {
                        ImageUrl = ImageHelper.SavePostedFile(PostImage, Server.MapPath("~/Images/Upload"))
                    });
                }
                
                model.PostedById = User.Identity.GetUserId();
                _postServiceAgent.SavePost(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult SuggestedUser()
        {
            var model = _userServiceAgent.GetAllUsers(5, User.Identity.GetUserId()).ToList();
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_SuggestedUsers.cshtml", model);
        }
        public ActionResult TopProfiles()
        {
            var model = _userServiceAgent.GetAllUsers(5, User.Identity.GetUserId());
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_TopProfiles.cshtml", model);
        }
    }
}