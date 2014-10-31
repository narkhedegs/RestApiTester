using System;
using FluentValidation;
using RestSharp;
using IRestRequest = RestApiTester.Common.IRestRequest;

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

            var uri = new Uri(restRequest.Url.Scheme + "://" + restRequest.Url.Path);

            restSharpRestRequest.Method =
                (Method) Enum.Parse(typeof (Method), restRequest.Method.ToString(), ignoreCase: true);

            restSharpRestRequest.Resource = uri.AbsolutePath.TrimStart('/');

            foreach (var header in restRequest.Headers)
            {
                restSharpRestRequest.AddHeader(header.Key, header.Value);
            }

            foreach (var queryParameter in restRequest.Url.QueryParameters)
            {
                restSharpRestRequest.AddParameter(queryParameter.Key, queryParameter.Value);
            }

            return restSharpRestRequest;
        }
    }
}
