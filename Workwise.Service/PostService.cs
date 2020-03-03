using System.Collections.Generic;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.Service.Interface;

namespace Workwise.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public void SavePost(Post post)
        {
            _postRepository.SavePost(post);
        }

        public IEnumerable<Post> GetLatestPostByUser(string UserId)
        {
            return _postRepository.GetLatestPostByUser(UserId);
        }

    }
}