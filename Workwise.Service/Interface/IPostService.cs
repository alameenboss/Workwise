using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Service.Interface
{
    public interface IPostService
    {
        void SavePost(Post post, string UserId);
        IEnumerable<Post> GetLatestPostByUser(string UserId);
    }
}