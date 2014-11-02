using Moq;
using NSpec;
using RestApiTester.Common;

namespace RestApiTester.Specifications
{
    public class rest_client_specifications : nspec
    {
        private IRestClient _restClient;
        private IRestRequest _restRequest;
        private Mock<IRestSharpRestClientBuilder> _restSharpRestClientBuilder;
        private Mock<IRestSharpRestRequestBuilder> _restSharpRestRequestBuilder;
        private Mock<IRestResponseBuilder> _restResponseBuilder;

        public void when_executing_rest_request()
        {
            before = () =>
            {
                var restSharpRestClient = new Mock<RestSharp.IRestClient>();
                _restSharpRestClientBuilder = new Mock<IRestSharpRestClientBuilder>();
                _restSharpRestClientBuilder.Setup(builder => builder.Build(It.IsAny<IRestRequest>())).Returns(restSharpRestClient.Object);

                _restSharpRestRequestBuilder = new Mock<IRestSharpRestRequestBuilder>();
                _restResponseBuilder = new Mock<IRestResponseBuilder>();
                _restClient = new RestClient(
                    _restSharpRestClientBuilder.Object,
                    _restSharpRestRequestBuilder.Object,
                    _restResponseBuilder.Object);
            };

            act = () => _restClient.Execute(_restRequest);

            it["should use RestSharpRestClientBuilder to build RestSharpRestClient"] =
                () => _restSharpRestClientBuilder.Verify(builder => builder.Build(It.IsAny<IRestRequest>()), Times.Once);
            it["should use RestSharpRestRequestBuilder to build RestSharpRestRequest"] =
                () =>
                    _restSharpRestRequestBuilder.Verify(builder => builder.Build(It.IsAny<IRestRequest>()), Times.Once);
            it["should use RestResponseBuilder to build RestResponse"] =
                () =>
                    _restResponseBuilder.Verify(
                        builder => builder.Build(It.IsAny<RestSharp.IRestResponse>(), It.IsAny<IRestRequest>()),
                        Times.Once);
        }
    }
}
