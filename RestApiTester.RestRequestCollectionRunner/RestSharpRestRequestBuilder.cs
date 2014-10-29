using FluentValidation;
using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestSharpRestRequestBuilder
    {
        RestSharp.IRestRequest Build(IRestRequest restRequest);
    }

    public class RestSharpRestRequestBuilder : IRestSharpRestRequestBuilder
    {
        private readonly IValidator<IRestRequest> _restRequestValidator;

        public RestSharpRestRequestBuilder(IValidator<IRestRequest> restRequestValidator)
        {
            _restRequestValidator = restRequestValidator;
        }

        public RestSharp.IRestRequest Build(IRestRequest restRequest)
        {
            _restRequestValidator.ValidateAndThrow(restRequest);

            var restSharpRestRequest = new RestSharp.RestRequest();

            return restSharpRestRequest;
        }
    }
}
