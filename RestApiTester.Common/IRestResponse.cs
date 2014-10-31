using System;
using System.Collections.Generic;
using System.Net;

namespace RestApiTester.Common
{
    public interface IRestResponse
    {
        IRestRequest Request { get; set; }
        string ContentType { get; set; }
        long ContentLength { get; set; }
        string ContentEncoding { get; set; }
        string Content { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string StatusDescription { get; set; }
        byte[] RawBytes { get; set; }
        string Server { get; set; }
        IDictionary<string, string> Headers { get; }
        ResponseStatus ResponseStatus { get; set; }
        string ErrorMessage { get; set; }
        Exception ErrorException { get; set; }
    }
}