using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequest
    {
        IDictionary<string,string> Headers { get; set; }
        RestRequestMethod Method { get; set; }
        string Body { get; set; }
        IUrl Url { get; set; }
    }
}
