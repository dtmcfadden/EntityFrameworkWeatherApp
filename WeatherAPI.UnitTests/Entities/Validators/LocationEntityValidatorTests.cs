namespace WeatherAPI.UnitTests.Entities.Validators;
public class LocationEntityValidatorTests(ITestOutputHelper outputHelper)
{
    private readonly LocationEntityValidator _locationEntityValidator = new();
    private readonly ITestOutputHelper _output = outputHelper;

    [Theory]
    [InlineData("test")]
    [InlineData("test12ABc")]
    [InlineData("test12ABc,")]
    public async Task Given_LocationEntityValidator_IsValid(string location)
    {
        // Arrange
        var locationEntity = new LocationEntity(location);

        // Act
        var response = await _locationEntityValidator.ValidateAsync(locationEntity);

        // Assert
        Assert.True(response.IsValid);
    }

    [Theory]
    [InlineData("", new string[] {
        "'Location' must be between 2 and 100 characters. You entered 0 characters.",
        "Please use only letters, numbers, or comma."
    })]
    [InlineData("a", new string[] {
        "'Location' must be between 2 and 100 characters. You entered 1 characters."
    })]
    [InlineData("a+$", new string[] {
        "Please use only letters, numbers, or comma."
    })]
    [InlineData("$", new string[] {
        "'Location' must be between 2 and 100 characters. You entered 1 characters.",
        "Please use only letters, numbers, or comma."
    })]
    public async Task Given_LocationEntityValidator_IsNotValid(
        string location,
        string[] errors)
    {
        // Arrange
        var locationEntity = new LocationEntity(location);

        // Act
        var response = await _locationEntityValidator.ValidateAsync(locationEntity);

        // Assert
        Assert.False(response.IsValid);
        Assert.Equal(response.Errors.Count, errors.Length);
        response.Errors.ForEach(e =>
        {
            _output.WriteLine(e.ErrorMessage.ToString());
            Assert.Contains(e.ErrorMessage.ToString(), errors);
        });
    }
}
