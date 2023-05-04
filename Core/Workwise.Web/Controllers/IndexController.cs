using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;
using Workwise.Web.Helper;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        private readonly IUserServiceAgent _userServiceAgent;
        private readonly IPostServiceAgent _postServiceAgent;
        private readonly IDefaultsHelper _defaultHelper;
        public IndexController(IUserServiceAgent userService, IPostServiceAgent postService, IDefaultsHelper defaultHelper)
        {
            _userServiceAgent = userService;
            _postServiceAgent = postService;
            _defaultHelper = defaultHelper;
        }

        public async Task<ActionResult> Index()
        {
            var myuserId = "alameen02111988";

            var model = _userServiceAgent.GetUserById(myuserId);
            model.Posts = _postServiceAgent.GetLatestPostByUser(myuserId).ToList() ?? new List<PostViewModel>();
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
                PostedBy = _defaultHelper.GetUser("alameen02111988"),
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
        public async Task<ActionResult> CreatePost(PostViewModel model, IFormFile PostImage)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                if (PostImage != null)
                {
                    var filePath = Path.GetTempFileName();
                    model.PostImages.Add(new ImageViewModel()
                    {
                        ImageUrl = ImageHelper.SavePostedFile(PostImage, filePath)
                    });
                }

                model.PostedById = "alameen02111988";
                _postServiceAgent.SavePost(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActionResult> SuggestedUser()
        {
            var model = _userServiceAgent.GetAllUsers(5, "alameen02111988").ToList();
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_SuggestedUsers.cshtml", model);
        }
        public ActionResult TopProfiles()
        {
            var model = _userServiceAgent.GetAllUsers(5, "alameen02111988");
            //var model = RandomUserGenerator.GetManyDummyUser(10);
            return PartialView(@"~\Views\Index\_TopProfiles.cshtml", model);
        }
    }
}