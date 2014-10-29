using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using RestApiTester.Common;

namespace RestApiTester
{
    public class RestRequestValidator : AbstractValidator<IRestRequest>
    {
        public RestRequestValidator()
        {
            RuleFor(request => request.Url).NotNull();
            RuleFor(request => request.Url).SetValidator(new UrlValidator());
        }

        public override ValidationResult Validate(IRestRequest instance)
        {
            return instance == null
                ? new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("restRequest", "restRequest cannot be null or empty.")
                })
                : base.Validate(instance);
        }
    }
}
