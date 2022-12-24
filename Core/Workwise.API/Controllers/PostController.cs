using Microsoft.AspNetCore.Mvc;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost("SavePost")]
        public async Task<IActionResult> SavePost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _ = await postService.SavePost(post);

            return Ok(post);
        }

        [HttpGet("GetLatestPostByUser")]
        public async Task<IActionResult> GetLatestPostByUser(string UserId)
        {
            IEnumerable<Post> result = await postService.GetLatestPostByUser(UserId);

            return result == null ? NotFound() : Ok(result);
        }
    }
}
