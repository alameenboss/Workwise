using System.Threading.Tasks;

namespace Workwise.ServiceAgent
{

    public interface IHttpClient
    {
        T GetAsync<T>(string requestUri);
        T PostDataAsync<T>(string requestUri, T value);
        U PostDataAsync<T, U>(string requestUri, T value);
    }
}
