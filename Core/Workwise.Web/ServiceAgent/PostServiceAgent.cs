using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class PostServiceAgent : IPostServiceAgent
    {
        private readonly IHttpClient _httpClient;
        public PostServiceAgent(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<PostViewModel> GetLatestPostByUser(string UserId)
        {
            return _httpClient.GetAsync<IEnumerable<PostViewModel>>(string.Format(Constent.Post.GetLatestPostByUser, UserId));
        }

        public void SavePost(PostViewModel post)
        {
            _httpClient.PostDataAsync(Constent.Post.SavePost, post);
        }
    }
}