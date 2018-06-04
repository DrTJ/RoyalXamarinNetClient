using System.Net;

namespace RoyalXamarinNetClient
{
    public class ResponseModelBase
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpStatusMessage { get; set; }
    }
}