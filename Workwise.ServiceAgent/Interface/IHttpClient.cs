using System.Net.Http;

namespace Workwise.ServiceAgent
{

    public interface IHttpClient
    {
        HttpResponseMessage PostData<T>(string requestUri, T value);
        T Get<T>(string requestUri);
    }
}
