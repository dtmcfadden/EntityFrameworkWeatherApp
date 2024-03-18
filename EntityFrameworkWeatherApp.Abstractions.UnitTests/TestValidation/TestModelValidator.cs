using EntityFrameworkWeatherApp.Abstractions.UnitTests.TestData;
using FluentValidation;

namespace EntityFrameworkWeatherApp.Abstractions.UnitTests.TestValidation;
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
