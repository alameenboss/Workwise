using System.Collections.Generic;
using System.Net.Http;
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
        public IHttpActionResult SavePost(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                _postService.SavePost(post);
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            return Ok();

        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Post>))]
        public IHttpActionResult GetLatestPostByUser(string UserId)
        {
            try
            {
                var result = _postService.GetLatestPostByUser(UserId);
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
