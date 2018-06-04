using System.Net.Http;

namespace RoyalXamarinNetClient
{
    public class RequestResult
    {
        #region Constructors

        public RequestResult(HttpResponseMessage response) 
        {
            this.IsSuccessful = response.IsSuccessStatusCode;
            this.StatusCode = response.StatusCode;
            this.Message = string.Empty;
        }

        #endregion

        #region Properties

        public System.Net.HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public HttpRequestMessage Data { get; set; }

        #endregion
    }

    public class RequestResult<TResponseType> : RequestResult
    {
        #region Constructors

        public RequestResult(HttpResponseMessage response) : base(response) {
            this.Data = default(TResponseType);
        }

        #endregion

        #region Properties

        public new TResponseType Data { get; set; }

        #endregion
    }
}