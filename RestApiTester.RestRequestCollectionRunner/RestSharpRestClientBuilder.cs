using System;
using FluentValidation;
using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestSharpRestClientBuilder
    {
        RestSharp.IRestClient Build(IRestRequest restRequest);
    }

    public class RestSharpRestClientBuilder : IRestSharpRestClientBuilder
    {
        private readonly IValidator<IRestRequest> _restRequestValidator;

        public RestSharpRestClientBuilder(IValidator<IRestRequest> restRequestValidator)
        {
            _restRequestValidator = restRequestValidator;
        }

        public RestSharp.IRestClient Build(IRestRequest restRequest)
        {
            _restRequestValidator.ValidateAndThrow(restRequest);

            var restClient = new RestSharp.RestClient();
            var uri = new Uri(restRequest.Url.Scheme + "://" + restRequest.Url.Path);
            restClient.BaseUrl = new Uri(string.Format("{0}://{1}", uri.Scheme, uri.Authority));

            return restClient;
        }
    }
}
