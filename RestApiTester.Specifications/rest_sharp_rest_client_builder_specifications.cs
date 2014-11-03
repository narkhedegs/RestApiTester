﻿using System;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NSpec;
using RestApiTester.Common;
using RestApiTester.Specifications.Helpers;

namespace RestApiTester.Specifications
{
    public class rest_sharp_rest_client_builder_specifications : nspec
    {
        private IRestSharpRestClientBuilder _builder;
        private IRestRequest _restRequest;
        private RestSharp.IRestClient _restClient;

        public void when_building_rest_sharp_rest_client()
        {
            const string domain = "api.gsn.com";
            Uri expectedBaseUri = null;

            before = () =>
            {
                var restRequestValidator = new Mock<IValidator<IRestRequest>>();
                restRequestValidator.Setup(validator => validator.Validate(It.IsAny<IRestRequest>()))
                    .Returns(new ValidationResult());

                _restRequest = RestRequestGenerator.Default()
                    .WithUrl(UrlGenerator.Default().WithPath(domain + "/users"));
                expectedBaseUri = new Uri(_restRequest.Url.Scheme + "://" + domain);

                _builder = new RestSharpRestClientBuilder(restRequestValidator.Object);
            };

            act = () => _restClient = _builder.Build(_restRequest);

            it["should populate BaseUrl"] = () => _restClient.BaseUrl.should_be(expectedBaseUri);
        }
    }
}
