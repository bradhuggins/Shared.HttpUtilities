#region Using Statements
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

namespace Shared.HttpUtilities
{
    public interface IRestClient
    {
        string ErrorMessage { get; set; }

        bool HasError { get; }

        //HttpResponseMessage Response { get; set; }

        Dictionary<string, string> Headers { get; set; }

        Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, HttpContent content = null, bool disableWait = false);

        //Task<Y> Get<Y>(string url);

        //Task<Y> Post<X, Y>(string url, X request);

        //Task<Y> Put<X, Y>(string url, X request);

        //Task<Y> Delete<Y>(string url);

    }
}
