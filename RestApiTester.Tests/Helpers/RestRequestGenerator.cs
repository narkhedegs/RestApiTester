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

        public static IRestRequest WithUrlPath(this IRestRequest request, string urlPath)
        {
            request.Url = UrlGenerator.Default().WithPath(urlPath);

            return request;
        }

        public static IRestRequest WithNoUrl(this IRestRequest request)
        {
            request.Url = null;

            return request;
        }        
        
        public static IRestRequest WithNoUrlScheme(this IRestRequest request)
        {
            request.Url = UrlGenerator.Default().WithNoScheme();

            return request;
        }

        public static IRestRequest WithNoUrlPath(this IRestRequest request)
        {
            request.Url = UrlGenerator.Default().WithNoPath();

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
