using EntityFrameworkWeatherApp.Infrastructure.UnitTests.TestData;
using EntityFrameworkWeatherApp.Infrastructure.UnitTests.TestValidation;
using FluentValidation.Results;

namespace EntityFrameworkWeatherApp.Infrastructure.UnitTests.Results;
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
        Assert.True(result.IsSuccess);
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
        Assert.True(result.IsFailure);
        Assert.Equal(1, result.Exception?.Count);
        Assert.Equal("Test Exception", result.GetException?.Message);
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
        Assert.True(result.IsFailure);
        Assert.Equal(1, result.Exception?.Count);
        Assert.Equal("NotEmptyValidator", result.GetValidationResult?.Errors[0].ErrorCode);
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
        Assert.True(result.IsFailure);
        Assert.Equal(2, result.Exception?.Count);
        Assert.Equal("Test Exception", result.GetException?.Message);
        Assert.Equal("NotEmptyValidator", result.GetValidationResult?.Errors[0].ErrorCode);
    }
}
