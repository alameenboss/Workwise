using System.Net.Http;
using System.Threading.Tasks;

namespace Workwise.ServiceAgent
{

    public interface IHttpClient
    {
        Task<T> PostDataAsync<T>(string requestUri, T value);
        void PostData<U>(string requestUri,U tValue);
        T Get<T>(string requestUri);
    }
}
