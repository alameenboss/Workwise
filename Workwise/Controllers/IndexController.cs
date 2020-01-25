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
            text.AddContentParagraphs(2, 2, 4, 5, 12);

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
            return PartialView("_PostDetail", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }
           
            try
            {
                postrepository.SavePost(model, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
            

        }

    }
}