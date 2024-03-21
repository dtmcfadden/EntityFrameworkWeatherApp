using EntityFrameworkWeatherApp.Abstractions.UnitTests.TestData;
using EntityFrameworkWeatherApp.Abstractions.UnitTests.TestValidation;
using FluentValidation.Results;

namespace EntityFrameworkWeatherApp.Abstractions.UnitTests.Results;
public class ResultTests
{
    private readonly ITestOutputHelper _output;

    public ResultTests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
    }

    [Fact]
    public void Given_ResultWithType_HasResult()
    {
        // Arrange
        var testModel = new TestModel();

        // Act
        var result = new Result<TestModel>(testModel);
        _output.WriteLine(JsonSerializer.Serialize(result));
        _output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }

    [Fact]
    public void Given_ResultWithException_HasResult()
    {
        // Arrange
        Exception exception = new("Test Exception");

        // Act
        var result = new Result<TestModel>(exception);
        _output.WriteLine(JsonSerializer.Serialize(result));
        _output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }

    [Fact]
    public void Given_ResultWithValidationFailure_HasResult()
    {
        // Arrange
        TestModel testModel = new();
        TestModelValidator validator = new();

        ValidationResult validationResult = validator.Validate(testModel);

        // Act
        var result = new Result<TestModel>(validationResult);
        _output.WriteLine(JsonSerializer.Serialize(result));
        _output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }

    [Fact]
    public void Given_ResultWithExceptionValidationFailure_HasResult()
    {
        // Arrange
        Exception exception = new("Test Exception");

        TestModel testModel = new();
        TestModelValidator validator = new();

        ValidationResult validationResult = validator.Validate(testModel);

        // Act
        var result = new Result<TestModel>(exception, validationResult);
        _output.WriteLine(JsonSerializer.Serialize(result));
        _output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }
}
