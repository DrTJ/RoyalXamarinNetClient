using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoyalXamarinNetClient
{
    public class ServiceClient
    {
        #region Fields

        private static HttpClient client;

        #endregion

        #region Properties

        public static HttpClient Client => client = client ?? new HttpClient();

        #endregion

        #region Constructor

        public ServiceClient(string serviceUrl)
        {
            Client.BaseAddress = new System.Uri(serviceUrl);
        }

        //public ServiceClient() : this(NetResources.ServiceBaseUrl) { }

        #endregion

        #region Methods

        public async Task<RequestResult<TResponseType>> Get<TResponseType>(string methodUrl, RequestParameters queryStringParameters = null) {
            var url = Client.BaseAddress + methodUrl + (queryStringParameters ?? new RequestParameters()).ToQueryString();
            
            HttpResponseMessage response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                Debug.WriteLine($"ServiceClient :: Get [{url}] succeeded!");
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponseType>(await response.Content.ReadAsStringAsync());
                return new RequestResult<TResponseType>(response) { Data = result };
            }

            Debug.WriteLine($"ServiceClient :: Get [{url}] failed!");
            return new RequestResult<TResponseType>(null);
        }

        public async Task<RequestResult> Post<TDataParamType>(string methodUrl, TDataParamType data, RequestParameters queryStringParameters = null) {
            var url = Client.BaseAddress + methodUrl + (queryStringParameters ?? new RequestParameters()).ToQueryString();

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync(url, content);
            return new RequestResult(response) { Data = response.RequestMessage };
        }

        public async Task<RequestResult> Put<TDataParamType>(string methodUrl, TDataParamType data, RequestParameters queryStringParameters) {
            var url = Client.BaseAddress + methodUrl + (queryStringParameters ?? new RequestParameters()).ToQueryString();

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync(url, content);
            return new RequestResult(response) { Data = response.RequestMessage };
        }

        public async Task<RequestResult> Delete<TDataParamType>(string methodUrl, RequestParameters queryStringParameters) {
            var url = Client.BaseAddress + methodUrl + (queryStringParameters ?? new RequestParameters()).ToQueryString();

            HttpResponseMessage response = await Client.DeleteAsync(url);
            return new RequestResult(response) { Data = response.RequestMessage };
        } 

        #endregion
    }
}
