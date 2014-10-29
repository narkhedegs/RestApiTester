using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IUrl
    {
        string Scheme { get; set; }
        string Path { get; set; }
        string QueryDelimiter { get; set; }
        IDictionary<string,string> QueryParameters { get; set; }
    }
}