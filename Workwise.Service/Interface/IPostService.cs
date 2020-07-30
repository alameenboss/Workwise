using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.Model;

namespace Workwise.Service.Interface
{
    public interface IPostService
    {
        Task<Post> SavePost(Post post);
        Task<IEnumerable<Post>> GetLatestPostByUser(string UserId);
    }
}