using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Workwise.Model;
using Workwise.ResultModel;
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
        public async Task<IHttpActionResult> SavePost(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                await _postService.SavePost(post);
                return Ok(post);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Post>))]
        public async Task<IHttpActionResult> GetLatestPostByUser(string UserId)
        {
            try
            {
                var result = await _postService.GetLatestPostByUser(UserId);
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException)
            {

                throw;
            }
        }
    }
}
