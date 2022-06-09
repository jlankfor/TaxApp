using System;
using System.Net.Http;

namespace JuniperTaxApp.Core.Exceptions
{
    public class BasicAPIClientException : Exception
    {
        public BasicAPIClientException(HttpResponseMessage responseMessage)
        {
            HttpResponseMessage = responseMessage;
        }

        public HttpResponseMessage HttpResponseMessage { get; private set; }
    }
}
