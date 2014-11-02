using FluentValidation.Results;
using NSpec;
using RestApiTester.Common;
using RestApiTester.Specifications.Helpers;

namespace RestApiTester.Specifications
{
    public class rest_request_validator_specifications : nspec
    {
        private RestRequestValidator _restRequestValidator;
        private ValidationResult _validationResult;
        private IRestRequest _restRequest;

        public void when_validating_rest_request()
        {
            before = () => { _restRequestValidator = new RestRequestValidator(); };

            act = () => _validationResult = _restRequestValidator.Validate(_restRequest);

            context["if restRequest parameter is null"] = () =>
            {
                before = () => _restRequest = null;

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for restRequest"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "restRequest");
            };

            context["if restRequest Url is null"] = () =>
            {
                before = () => _restRequest = RestRequestGenerator.Default().WithUrl(null);

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for Url"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "Url");
            };

            context["if restRequest Url Scheme is null"] = () =>
            {
                before =
                    () => _restRequest = RestRequestGenerator.Default().WithUrl(UrlGenerator.Default().WithNoScheme());

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for Url Scheme"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "Url.Scheme");
            };

            context["if restRequest Url Path is null"] = () =>
            {
                before =
                    () => _restRequest = RestRequestGenerator.Default().WithUrl(UrlGenerator.Default().WithNoPath());

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for Url Path"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "Url.Path");
            };
        }
    }
}
