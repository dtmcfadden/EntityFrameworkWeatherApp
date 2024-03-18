namespace WeatherAPI.IntegrationTests.Entities;
public class LatLongEntityTests(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _output = outputHelper;

    [Theory]
    [InlineData(-90f, -180f, false)]
    [InlineData(90f, null, true)]
    [InlineData(null, 180f, true)]
    [InlineData(null, null, true)]
    public void Given_LatLongEntity_IsEmptyCheck(
        float? latitude,
        float? longitude,
        bool isEmptyCheck)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var response = latLongEntity.IsEmpty();

        // Assert
        Assert.Equal(isEmptyCheck, response);
    }

    [Theory]
    [InlineData(-90f, -180f, "-90,-180")]
    [InlineData(90f, null, "90,")]
    [InlineData(null, 180f, ",180")]
    [InlineData(null, null, ",")]
    public void Given_LatLongEntity_ToStringCheck(
        float? latitude,
        float? longitude,
        string stringCheck)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var response = latLongEntity.ToString();

        // Assert
        Assert.Equal(stringCheck, response);
    }
}
