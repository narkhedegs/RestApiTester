using System;
using FluentValidation.Results;
using Moq;
using NSpec;
using RestApiTester.Tests.Helpers;
using RestSharp;
using IRestRequest = RestApiTester.Common.IRestRequest;
using FluentValidation;

namespace RestApiTester.Tests
{
    public class rest_sharp_rest_client_builder_specifications : nspec
    {
        private IRestSharpRestClientBuilder _builder;
        private IRestRequest _restRequest;
        private IRestClient _restClient;

        public void when_building_rest_sharp_rest_client()
        {
            const string domain = "api.gsn.com";
            string expectedBaseUrl = null;

            before = () =>
            {
                var restRequestValidator = new Mock<IValidator<IRestRequest>>();
                restRequestValidator.Setup(validator => validator.Validate(It.IsAny<IRestRequest>()))
                    .Returns(new ValidationResult());

                _restRequest = RestRequestGenerator.Default().WithUrlPath(domain + "/users");
                expectedBaseUrl = _restRequest.Url.Scheme + "://" + domain;

                _builder = new RestSharpRestClientBuilder(restRequestValidator.Object);
            };

            act = () => _restClient = _builder.Build(_restRequest);

            it["should populate BaseUrl correctly"] = () => _restClient.BaseUrl.should_be(expectedBaseUrl);
        }
    }
}
