using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.Service.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class IndexController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        public IndexController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public ActionResult Index()
        {
            var myuserId = User.Identity.GetUserId();

            var model = _userService.GetUserById(myuserId);
            model.Posts = _postService.GetLatestPostByUser(myuserId).ToList();
            model.Following = _userService.FollowingList(myuserId, myuserId).Select(x => x.UserInfo).ToList();
            model.Followers = _userService.FollowersList(myuserId, myuserId).Select(x => x.UserInfo).ToList();

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
             
            var model = new Post()
            {
                PostedOn = DateTime.Now.AddSeconds(-90),
                PostedBy = SessionHelper.GetUser(User.Identity.GetUserId()),
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
                _postService.SavePost(model, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult SuggestedUser()
        {
            var model = _userService.GetAllUsers(5, User.Identity.GetUserId()).ToList();
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_SuggestedUsers.cshtml", model);
        }
        public ActionResult TopProfiles()
        {
            var model = _userService.GetAllUsers(5, User.Identity.GetUserId());
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_TopProfiles.cshtml", model);
        }
    }
}