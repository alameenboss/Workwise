using System.Net.Http;
using System.Threading.Tasks;

namespace Workwise.ServiceAgent
{

    public interface IHttpClient
    {
        Task<T> PostDataAsync<T>(string requestUri, T value);
        //T PostData<T,U>(string requestUri, U value);
        //T PostData<T,U,V>(string requestUri, U value,V value2);
        T Get<T>(string requestUri);
    }
}
