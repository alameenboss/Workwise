using System.Net.Http;
using System.Threading.Tasks;

namespace Workwise.ServiceAgent
{

    public interface IHttpClient
    {
        T PostData<T>(string requestUri, T value);
        Task<U> PostDataAsync<T, U>(string requestUri, T value);
        T Get<T>(string requestUri);
    }
}
