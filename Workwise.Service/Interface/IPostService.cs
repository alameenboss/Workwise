using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Service.Interface
{
    public interface IPostService
    {
        void SavePost(Post post);
        IEnumerable<Post> GetLatestPostByUser(string UserId);
    }
}