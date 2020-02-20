using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Data.Interface
{
    public interface IPostRepository
    {
        void SavePost(Post post, string UserId);
        IEnumerable<Post> GetLatestPostByUser(string UserId);
    }
}