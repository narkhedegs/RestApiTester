using System;
using System.Collections.Generic;
using System.Net;
using RestApiTester.Common;
using IRestRequest = RestApiTester.Common.IRestRequest;

namespace RestApiTester
{
    public class RestResponse : IRestResponse
    {
        public RestResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        public IRestRequest Request { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string ContentEncoding { get; set; }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public byte[] RawBytes { get; set; }
        public string Server { get; set; }
        public IDictionary<string,string> Headers { get; private set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ErrorException { get; set; }
        public long ExecutionTime { get; set; }
    }
}
