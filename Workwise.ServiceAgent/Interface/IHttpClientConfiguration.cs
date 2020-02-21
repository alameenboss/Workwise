using System.Net.Http;

namespace Workwise.ServiceAgent
{

    public interface IHttpClientConfiguration
    {
        HttpResponseMessage PostData<T>(string requestUri, T value);
        T PostData<T, U>(string requestUri, T value);
        R PostData<R, Pr, Pre>(string requestUri, Pre value);
        U PostDataGetCustomResponse<T, U>(string requestUri, T value);

        T Get<T, U>(string requestUri);
        T GetCustomResponse<T>(string requestUri);
    }
}
