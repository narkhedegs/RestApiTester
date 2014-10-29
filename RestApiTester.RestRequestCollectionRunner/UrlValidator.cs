using FluentValidation;
using RestApiTester.Common;

namespace RestApiTester
{
    public class UrlValidator : AbstractValidator<IUrl>
    {
        public UrlValidator()
        {
            RuleFor(url => url.Scheme).NotEmpty();
            RuleFor(url => url.Path).NotEmpty();
        }
    }
}