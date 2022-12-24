using Workwise.API.Models;

namespace Workwise.API.Service.Interface
{
    public interface IPostService
    {
        Task<Post> SavePost(Post post);
        Task<IEnumerable<Post>> GetLatestPostByUser(string UserId);
    }
}