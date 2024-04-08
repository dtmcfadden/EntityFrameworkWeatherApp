using EntityFrameworkWeatherApp.Infrastructure.UnitTests.TestData;
using FluentValidation;

namespace EntityFrameworkWeatherApp.Infrastructure.UnitTests.TestValidation;
public class TestModelValidator : AbstractValidator<TestModel>
{
    public TestModelValidator()
    {
        RuleFor(a => a.TestString)
            .NotEmpty()
            .NotNull();

        RuleFor(a => a.TestEmptyString)
            .NotEmpty()
            .NotNull();
    }
}
