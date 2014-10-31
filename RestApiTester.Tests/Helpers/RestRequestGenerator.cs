using System.Collections.Generic;
using RestApiTester.Common;

namespace RestApiTester.Tests.Helpers
{
    public static class RestRequestGenerator
    {
        public static IRestRequest Default()
        {
            var request = new FakeRestRequest
            {
                Method = RestRequestMethod.Get,
                Headers = new Dictionary<string, string>
                {
                    {"SampleHeader", "SampleHeaderValue"}
                },
                Url = UrlGenerator.Default()
            };

            return request;
        }

        public static IRestRequest WithHeader(this IRestRequest request, string key, string value)
        {
            request.Headers.Add(new KeyValuePair<string, string>(key,value));

            return request;
        }

        public static IRestRequest WithHeaders(this IRestRequest request, IDictionary<string,string> headers)
        {
            request.Headers = headers;

            return request;
        }

        public static IRestRequest WithMethod(this IRestRequest request, RestRequestMethod method)
        {
            request.Method = method;

            return request;
        }
        
        public static IRestRequest WithUrl(this IRestRequest request, IUrl url)
        {
            request.Url = url;

            return request;
        }
    }

    public class FakeRestRequest : IRestRequest
    {
        public IDictionary<string, string> Headers { get; set; }
        public RestRequestMethod Method { get; set; }
        public string Body { get; set; }
        public IUrl Url { get; set; }
    }
}
