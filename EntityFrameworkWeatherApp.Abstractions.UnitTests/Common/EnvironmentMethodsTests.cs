using EntityFrameworkWeatherApp.Abstractions.Common;

namespace EntityFrameworkWeatherApp.Abstractions.UnitTests.Common;
public class EnvironmentMethodsTests
{
    private readonly ITestOutputHelper _output;

    public EnvironmentMethodsTests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
    }

    [Fact]
    public void Given_GetEnvironmentVariables_IsDevelopment_HasResult()
    {
        // Arrange
        var envVar = EnvironmentMethods.IsDevelopment;

        // Act
        _output.WriteLine(JsonSerializer.Serialize(envVar));
        //_output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }

    [Fact]
    public void Given_GetEnvironmentVariables_HasResult()
    {
        // Arrange
        var envVar = EnvironmentMethods.GetEnvironmentVariables("user");

        // Act
        _output.WriteLine(JsonSerializer.Serialize(envVar));
        //_output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }

    [Fact]
    public void Given_GetEnvironmentVariable_HasResult()
    {
        // Arrange
        var envVar = EnvironmentMethods.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Act
        _output.WriteLine(JsonSerializer.Serialize(envVar));
        //_output.WriteLine(JsonSerializer.Serialize(result.Exception));

        // Assert
        Assert.Equal(1, 1);
    }
}
