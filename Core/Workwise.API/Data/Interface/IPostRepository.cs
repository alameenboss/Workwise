using Workwise.API.Models;

namespace Workwise.API.Data.Interface
{
    public interface IPostRepository
    {
        Task<Post> SavePost(Post post);
        Task<IEnumerable<Post>> GetLatestPostByUser(string UserId);
    }
}