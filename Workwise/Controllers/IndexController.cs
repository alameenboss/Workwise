using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Helper;
using Workwise.Data.Models;


namespace Workwise.Controllers
{
    [Authorize]
    public class IndexController : BaseController
    {
        private readonly IUserRepository _userProfileRepo;
        private readonly IPostRepository _postrepository;
        public IndexController(IUserRepository userProfileRepo, IPostRepository postrepository)
        {
            _userProfileRepo = userProfileRepo;
            _postrepository = postrepository;
        }

        public ActionResult Index()
        {
            var model = _postrepository.GetLatestPostByUser(User.Identity.GetUserId());
            if (model == null)
            {
                model = new List<Post>();
            }
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
                PostImages = new List<ImageModel>()
                {
                    new ImageModel()
                    {
                        ImageUrl = $"/images/postimages/image{RandomGenerator.GenerateLockedRandom(1, 14)}.jpg"
                    }
                }
            };
            return PartialView(@"~\Views\Index\_PostDetail.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post model, HttpPostedFileBase PostImage)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }
           
            try
            {
                if (PostImage != null)
                {
                    model.PostImages.Add(new ImageModel()
                    {
                        ImageUrl = ImageHelper.SavePostedFile(PostImage, Server.MapPath("~/Images/Upload"))
                    });
                }   
                _postrepository.SavePost(model, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult SuggestedUser()
        {
            var model = _userProfileRepo.GetAllUsers(5, User.Identity.GetUserId()).ToList();
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_SuggestedUsers.cshtml", model);
        }
        public ActionResult TopProfiles()
        {
            var model = _userProfileRepo.GetAllUsers(5, User.Identity.GetUserId());
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_TopProfiles.cshtml", model);
        }
    }
}