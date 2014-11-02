using FluentValidation.Results;
using NSpec;
using RestApiTester.Common;
using RestApiTester.Specifications.Helpers;

namespace RestApiTester.Specifications
{
    public class url_validator_specifications : nspec
    {
        private UrlValidator _urlValidator;
        private IUrl _url;
        private ValidationResult _validationResult;

        public void when_validating_url()
        {
            before = () => _urlValidator = new UrlValidator();

            act = () => _validationResult = _urlValidator.Validate(_url);

            context["if Url Scheme is null"] = () =>
            {
                before = () => _url = UrlGenerator.Default().WithNoScheme();

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for Scheme"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "Scheme");
            };

            context["if Url Path is null"] = () =>
            {
                before = () => _url = UrlGenerator.Default().WithNoPath();

                it["should fail validation"] = () => _validationResult.IsValid.should_be_false();
                it["should contain error for Path"] =
                    () => _validationResult.Errors.should_contain(error => error.PropertyName == "Path");
            };
        }
    }
}
