using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestClient
    {
        IRestResponse Execute(IRestRequest request);
    }

    public class RestClient : IRestClient
    {
        private readonly IRestSharpRestClientBuilder _restSharpRestClientBuilder;
        private readonly IRestSharpRestRequestBuilder _restSharpRestRequestBuilder;
        private readonly IRestResponseBuilder _restResponseBuilder;

        public RestClient(
            IRestSharpRestClientBuilder restSharpRestClientBuilder,
            IRestSharpRestRequestBuilder restSharpRestRequestBuilder,
            IRestResponseBuilder restResponseBuilder)
        {
            _restSharpRestClientBuilder = restSharpRestClientBuilder;
            _restSharpRestRequestBuilder = restSharpRestRequestBuilder;
            _restResponseBuilder = restResponseBuilder;
        }

        public IRestResponse Execute(IRestRequest request)
        {
            var restClient = _restSharpRestClientBuilder.Build(request);
            var restSharpRestRequest = _restSharpRestRequestBuilder.Build(request);
            var restSharpRestResponse = restClient.Execute(restSharpRestRequest);
            var restResponse = _restResponseBuilder.Build(restSharpRestResponse,request);

            return restResponse;
        }
    }
}
