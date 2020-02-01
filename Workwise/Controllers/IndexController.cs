using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Models;
using Microsoft.AspNet.Identity;
using Workwise.Helper;
using System.IO;

namespace Workwise.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        private readonly PostRepository postrepository;
        public IndexController()
        {
            postrepository = new PostRepository();
        }

        // GET: Conpanies
        public ActionResult Index()
        {
            var model = postrepository.GetLatestPostByUser(User.Identity.GetUserId());
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
                LikeCount = RandomGenerator.GenerateLockedRandom(10, 1000),
                ViewCount = RandomGenerator.GenerateLockedRandom(10, 1000),
                CommentCount = RandomGenerator.GenerateLockedRandom(10, 1000),
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
                    var postedImage = new ImageModel();

                    var extention = Path.GetExtension(PostImage.FileName);

                    var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace(" ", "") + "." + extention;
                    var path = Path.Combine(Server.MapPath("~/Images/Upload"), fileName);
                    var imgUrl = @"/Images/Upload/" + fileName;
                    PostImage.SaveAs(path);
                    postedImage.ImageUrl = imgUrl;
                    model.PostImages = new List<ImageModel>();
                    model.PostImages.Add(postedImage);
                }
                
                postrepository.SavePost(model, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
            

        }


        public ActionResult SuggestedUser()
        {
            var userprofilerepo = new UserProfileRepository();
            var model = userprofilerepo.GetAllUsers().Where(x => x.UserId != User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Index\_SuggestedUsers.cshtml", model);
        }
        public ActionResult TopProfiles()
        {
            var userprofilerepo = new UserProfileRepository();
            var model = userprofilerepo.GetAllUsers().Where(x => x.UserId != User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Index\_TopProfiles.cshtml", model);
        }
    }
}