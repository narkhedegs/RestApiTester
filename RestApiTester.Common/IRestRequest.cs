using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequest
    {
        IDictionary<string,string> Headers { get; set; }
        RestRequestMethod Method { get; set; }
        string Boby { get; set; }
        string Url { get; set; }
    }
}
