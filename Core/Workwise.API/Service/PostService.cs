using Workwise.API.Data.Interface;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> SavePost(Post post)
        {
            return await _postRepository.SavePost(post);
        }

        public async Task<IEnumerable<Post>> GetLatestPostByUser(string UserId)
        {
            return await _postRepository.GetLatestPostByUser(UserId);
        }

    }
}