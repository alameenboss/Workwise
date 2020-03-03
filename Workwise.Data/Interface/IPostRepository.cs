using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Data.Interface
{
    public interface IPostRepository
    {
        void SavePost(Post post);
        IEnumerable<Post> GetLatestPostByUser(string UserId);
    }
}