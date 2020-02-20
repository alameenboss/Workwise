using System;
using System.Collections.Generic;
using System.Linq;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.Service.Interface;

namespace Workwise.Data
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public void SavePost(Post post, string UserId)
        {
            _postRepository.SavePost(post, UserId);
        }

        public IEnumerable<Post> GetLatestPostByUser(string UserId)
        {
            return _postRepository.GetLatestPostByUser(UserId);
        }

    }
}