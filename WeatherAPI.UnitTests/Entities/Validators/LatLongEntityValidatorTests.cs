namespace WeatherAPI.UnitTests.Entities.Validators;
public class LatLongEntityValidatorTests(ITestOutputHelper outputHelper)
{
    private readonly LatLongEntityValidator _latLongEntityValidator = new();
    private readonly ITestOutputHelper _output = outputHelper;

    [Theory]
    [InlineData(-90, -180)]
    [InlineData(90, 180)]
    public async Task Given_LatLongEntityValidator_IsValid(
        float latitude,
        float longitude)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var response = await _latLongEntityValidator.ValidateAsync(latLongEntity);

        // Assert
        Assert.True(response.IsValid);
    }

    [Theory]
    [InlineData(91, 181, new string[] {
        "'Latitude' must be between -90 and 90. You entered 91.",
        "'Longitude' must be between -180 and 180. You entered 181."
    })]
    [InlineData(-91, -181, new string[] {
        "'Latitude' must be between -90 and 90. You entered -91.",
        "'Longitude' must be between -180 and 180. You entered -181."
    })]
    public async Task Given_LatLongEntityValidator_IsNotValid(
        float latitude,
        float longitude,
        string[] errors)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var response = await _latLongEntityValidator.ValidateAsync(latLongEntity);

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
