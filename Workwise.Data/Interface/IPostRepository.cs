using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.Data.Interface
{
    public interface IPostRepository
    {
        Task<Post> SavePost(Post post);
        Task<IEnumerable<Post>> GetLatestPostByUser(string UserId);
    }
}