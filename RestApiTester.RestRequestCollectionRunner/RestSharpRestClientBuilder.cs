using System;
using FluentValidation;
using RestSharp;
using IRestRequest = RestApiTester.Common.IRestRequest;

namespace RestApiTester
{
    public interface IRestSharpRestClientBuilder
    {
        IRestClient Build(IRestRequest restRequest);
    }

    public class RestSharpRestClientBuilder : IRestSharpRestClientBuilder
    {
        private readonly IValidator<IRestRequest> _restRequestValidator;

        public RestSharpRestClientBuilder(IValidator<IRestRequest> restRequestValidator)
        {
            _restRequestValidator = restRequestValidator;
        }

        public IRestClient Build(IRestRequest restRequest)
        {
            _restRequestValidator.ValidateAndThrow(restRequest);

            var restClient = new RestClient();
            var uri = new Uri(restRequest.Url.Scheme + "://" + restRequest.Url.Path);
            restClient.BaseUrl = string.Format("{0}://{1}", uri.Scheme, uri.Authority);

            return restClient;
        }
    }
}
