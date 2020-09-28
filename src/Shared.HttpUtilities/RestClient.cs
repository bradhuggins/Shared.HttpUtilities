#region Using Statements
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

namespace Shared.HttpUtilities
{
    public class RestClient : IRestClient
    {
        private readonly static HttpClientHandler _handler = new HttpClientHandler { };

        private readonly static HttpClient _systemHttpClient = new HttpClient(_handler);

        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(this.ErrorMessage); }
        }

        public Dictionary<string,string> Headers { get; set; }

        private HttpRequestMessage AddHeaders(HttpRequestMessage request)
        {
            if (this.Headers != null)
            {
                foreach (var item in this.Headers)
                {
                    if (!request.Headers.Contains(item.Key))
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
            }
            return request;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, HttpContent content = null, bool disableWait = false)
        {
            HttpResponseMessage toReturn = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
                request = AddHeaders(request);
                if (content != null)
                {
                    request.Content = content;
                }
                if (disableWait)
                {
                    toReturn = await _systemHttpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
                }
                else
                {
                    toReturn = await _systemHttpClient.SendAsync(request);
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

        #region Unused-Extra

        //public HttpResponseMessage Response { get; set; }

        //public async Task<Y> Get<Y>(string url)
        //{
        //    await SendAsync(HttpMethod.Get, url);
        //    Y toReturn = await this.ParseResponseObject<Y>();
        //    return toReturn;
        //}

        //public async Task<Y> Post<X, Y>(string url, X request)
        //{
        //    HttpContent content = JsonHelper.SerializeContent(request);
        //    await SendAsync(HttpMethod.Post, url, content);
        //    Y toReturn = await this.ParseResponseObject<Y>();
        //    return toReturn;
        //}

        //public async Task<Y> Put<X, Y>(string url, X request)
        //{
        //    HttpContent content = JsonHelper.SerializeContent(request);
        //    await SendAsync(HttpMethod.Put, url, content);
        //    Y toReturn = await this.ParseResponseObject<Y>();
        //    return toReturn;
        //}

        //public async Task<Y> Delete<Y>(string url)
        //{
        //    await SendAsync(HttpMethod.Delete, url);
        //    Y toReturn = await this.ParseResponseObject<Y>();
        //    return toReturn;
        //}

        //private async Task<Y> ParseResponseObject<Y>()
        //{
        //    Y toReturn = default(Y);
        //    if(this.Response == null)
        //    {
        //        return toReturn;
        //    }    
        //    try
        //    {
        //        if (!this.Response.IsSuccessStatusCode)
        //        {
        //            this.ErrorMessage = this.Response.StatusCode.ToString() + " " + this.Response.ReasonPhrase;
        //        }
        //        else
        //        {
        //            toReturn = await JsonHelper.DeserializeResponse<Y>(this.Response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorMessage = ex.ToString();
        //    }
        //    return toReturn;
        //} 
        #endregion

    }
}
