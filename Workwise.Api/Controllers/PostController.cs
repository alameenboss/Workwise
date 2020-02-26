using System.Collections.Generic;
using System.Web.Http;
using Workwise.Model;
using Workwise.Service.Interface;
namespace Workwise.Api.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService _postService = null;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public void SavePost(Post post, string UserId)
        {
            _postService.SavePost(post, UserId);
        }


        public IEnumerable<Post> GetLatestPostByUser(string UserId)
        {
            return _postService.GetLatestPostByUser(UserId);
        }
    }
}
