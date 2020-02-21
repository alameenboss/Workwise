using System.Collections.Generic;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent.Interface
{
    public interface IPostServiceAgent
    {
        void SavePost(PostViewModel post, string UserId);
        IEnumerable<PostViewModel> GetLatestPostByUser(string UserId);
    }
}