using System;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NSpec;
using RestApiTester.Common;

namespace RestApiTester.Tests
{
    public class rest_sharp_rest_request_builder_specifications : nspec
    {
        private IRestSharpRestRequestBuilder _builder;
        private IRestRequest _restRequest;
        private RestSharp.IRestRequest _restSharpRestRequest;

        public void when_building_rest_sharp_rest_request()
        {
            before = () =>
            {
                var restRequestValidator = new Mock<IValidator<IRestRequest>>();
                restRequestValidator.Setup(validator => validator.Validate(It.IsAny<IRestRequest>()))
                    .Returns(new ValidationResult());

                _builder = new RestSharpRestRequestBuilder(restRequestValidator.Object);
            };

            act = () => _restSharpRestRequest = _builder.Build(_restRequest);

            context["if restRequest parameter is null"] = () =>
            {
                before = () => _restRequest = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };
        }
    }
}
