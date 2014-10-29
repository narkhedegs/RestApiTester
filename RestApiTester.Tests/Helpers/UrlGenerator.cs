using System.Collections.Generic;
using RestApiTester.Common;

namespace RestApiTester.Tests.Helpers
{
    public static class UrlGenerator
    {
        public static IUrl Default()
        {
            var url = new FakeUrl {Scheme = "http", Path = "api.gsn.com/users"};

            return url;
        }

        public static IUrl WithPath(this IUrl url,string path)
        {
            url.Path = path;
            return url;
        }

        public static IUrl WithNoPath(this IUrl url)
        {
            url.Path = string.Empty;
            return url;
        }

        public static IUrl WithNoScheme(this IUrl url)
        {
            url.Scheme = string.Empty;
            return url;
        }
    }

    public class FakeUrl : IUrl
    {
        public string Scheme { get; set; }
        public string Path { get; set; }
        public string QueryDelimiter { get; set; }
        public IDictionary<string, string> QueryParameters { get; set; }
    }
}
