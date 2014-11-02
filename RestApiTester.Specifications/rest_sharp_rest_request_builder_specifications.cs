using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NSpec;
using NSpec.Domain.Extensions;
using RestApiTester.Common;
using RestApiTester.Specifications.Helpers;
using RestSharp;
using IRestRequest = RestApiTester.Common.IRestRequest;

namespace RestApiTester.Specifications
{
    public class rest_sharp_rest_request_builder_specifications : nspec
    {
        private IRestSharpRestRequestBuilder _builder;
        private IRestRequest _restRequest;
        private RestSharp.IRestRequest _restSharpRestRequest;

        public void when_building_rest_sharp_rest_request()
        {
            const string expectedMethod = "Get";
            const string expectedResource = "users/1234";
            var expectedQueryParameters = new Dictionary<string, string>
            {
                {"Parameter1", "Parameter1Value"},
                {"Parameter2", "Parameter2Value"},
            };

            before = () =>
            {
                var restRequestValidator = new Mock<IValidator<IRestRequest>>();
                restRequestValidator.Setup(validator => validator.Validate(It.IsAny<IRestRequest>()))
                    .Returns(new ValidationResult());

                _builder = new RestSharpRestRequestBuilder(restRequestValidator.Object);

                _restRequest =
                    RestRequestGenerator.Default()
                        .WithMethod(
                            (RestRequestMethod) Enum.Parse(typeof (RestRequestMethod), expectedMethod, ignoreCase: true))
                        .WithUrl(
                            UrlGenerator.Default()
                                .WithPath("www.sample.com/" + expectedResource)
                                .WithQueryParameters(expectedQueryParameters));
            };

            act = () => _restSharpRestRequest = _builder.Build(_restRequest);

            it["should populate Method"] =
                () =>
                    _restSharpRestRequest.Method.should_be(
                        (Method) Enum.Parse(typeof (Method), expectedMethod, ignoreCase: true));
            it["should populate resource"] = () => _restSharpRestRequest.Resource.should_be(expectedResource);
            it["should populate Query Parameters"] =
                () =>
                    expectedQueryParameters.Each(
                        queryParameter =>
                            _restSharpRestRequest.Parameters.should_contain(
                                parameter =>
                                    parameter.Name == queryParameter.Key &&
                                    parameter.Value.ToString() == queryParameter.Value));
        }
    }
}
