using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequest
    {
        IEnumerable<KeyValuePair<string,string>> Headers { get; set; }
        RestRequestMethod Method { get; set; }
        string Boby { get; set; }
        string Url { get; set; }
    }
}
