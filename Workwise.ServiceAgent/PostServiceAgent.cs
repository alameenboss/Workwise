using System.Collections.Generic;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class PostServiceAgent : IPostServiceAgent
    {
        public IEnumerable<PostViewModel> GetLatestPostByUser(string UserId)
        {
            throw new System.NotImplementedException();
        }

        public void SavePost(PostViewModel post, string UserId)
        {
            throw new System.NotImplementedException();
        }
    }
}