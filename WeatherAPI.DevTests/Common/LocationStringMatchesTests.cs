using WeatherAPI.Common;

namespace WeatherAPI.DevTests.Common;
public class LocationStringMatchesTests
{
    private readonly ITestOutputHelper _output;
    private readonly LocationStringMatches _locationStringMatches;

    public LocationStringMatchesTests(ITestOutputHelper outputHelper)
    {
        _locationStringMatches = new LocationStringMatches();
        _output = outputHelper;
    }

    [Theory]
    [InlineData("11")]
    [InlineData("111")]
    [InlineData("1111")]
    [InlineData("11111")]
    [InlineData("111111")]
    [InlineData("11-111")]
    [InlineData("11-111111")]
    [InlineData("11111-111")]
    [InlineData("11111-111111")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithDash_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithDash, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("11,cc")]
    [InlineData("111,cc")]
    [InlineData("1111,cc")]
    [InlineData("11111,cc")]
    [InlineData("111111,cc")]
    [InlineData("11-111,cc")]
    [InlineData("11-111111,cc")]
    [InlineData("11111-111,cc")]
    [InlineData("11111-111111,cc")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithDashAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithDashAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("11111 (111 11)")]
    [InlineData("111111 (111-111)")]
    [InlineData("1111111 (111-1111)")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithParenthesis_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithParenthesis, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("11111 (111 11),aa")]
    [InlineData("111111 (111-111),aa")]
    [InlineData("1111111 (111-1111),aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithParenthesisAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithParenthesisAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("97111")]
    [InlineData("98111")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberStatsWithNine_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberStatsWithNine, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("97111,aa")]
    [InlineData("98111,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberStatsWithNineAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberStatsWithNineAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("ASCN 1ZZ")]
    [InlineData("BIQQ 1ZZ")]
    [InlineData("BIQQ1ZZ")]
    [InlineData("BBND 1ZZ")]
    [InlineData("BBND1ZZ")]
    [InlineData("FIQQ 1ZZ")]
    [InlineData("FIQQ1ZZ")]
    [InlineData("PCRN 1ZZ")]
    [InlineData("PCRN1ZZ")]
    [InlineData("STHL 1ZZ")]
    [InlineData("STHL1ZZ")]
    [InlineData("SIQQ 1ZZ")]
    [InlineData("SIQQ1ZZ")]
    [InlineData("TDCU 1ZZ")]
    [InlineData("TDCU1ZZ")]
    [InlineData("TKCA 1ZZ")]
    [InlineData("TKCA1ZZ")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPLettersWith1ZZ_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPLettersWith1ZZ, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("ASCN 1ZZ,aa")]
    [InlineData("BIQQ 1ZZ,aa")]
    [InlineData("BIQQ1ZZ,aa")]
    [InlineData("BBND 1ZZ,aa")]
    [InlineData("BBND1ZZ,aa")]
    [InlineData("FIQQ 1ZZ,aa")]
    [InlineData("FIQQ1ZZ,aa")]
    [InlineData("PCRN 1ZZ,aa")]
    [InlineData("PCRN1ZZ,aa")]
    [InlineData("STHL 1ZZ,aa")]
    [InlineData("STHL1ZZ,aa")]
    [InlineData("SIQQ 1ZZ,aa")]
    [InlineData("SIQQ1ZZ,aa")]
    [InlineData("TDCU 1ZZ,aa")]
    [InlineData("TDCU1ZZ,aa")]
    [InlineData("TKCA 1ZZ,aa")]
    [InlineData("TKCA1ZZ,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPLettersWith1ZZAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPLettersWith1ZZAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("ad111")]
    [InlineData("az1111")]
    [InlineData("az11111")]
    [InlineData("AI-2640")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPStartsWithAHasNumbers_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPStartsWithAHasNumbers, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("ad111,aa")]
    [InlineData("az1111,aa")]
    [InlineData("az11111,aa")]
    [InlineData("AI-2640,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPStartsWithAHasNumbersAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPStartsWithAHasNumbersAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("111 11")]
    [InlineData("111 111")]
    [InlineData("1111 111")]
    [InlineData("1111 1111")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithSpace_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithSpace, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("111 11,aa")]
    [InlineData("111 111,aa")]
    [InlineData("1111 111,aa")]
    [InlineData("1111 1111,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPNumberWithSpaceAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPNumberWithSpaceAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("vg1111")]
    [InlineData("vc1111")]
    [InlineData("bbb 1111")]
    [InlineData("bbb1111")]
    [InlineData("si1111")]
    [InlineData("si 1111")]
    [InlineData("ky1-1111")]
    [InlineData("ky1 1111")]
    [InlineData("ky11111")]
    [InlineData("lv-1111")]
    [InlineData("lv 1111")]
    [InlineData("lv1111")]
    [InlineData("md-1111")]
    [InlineData("md 1111")]
    [InlineData("md1111")]
    [InlineData("MSR 1110-1350")]
    [InlineData("MSR1110-1350")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPEndsWithFourNumbers_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPEndsWithFourNumbers, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("vg1111,aa")]
    [InlineData("vc1111,aa")]
    [InlineData("bbb 1111,aa")]
    [InlineData("bbb1111,aa")]
    [InlineData("si1111,aa")]
    [InlineData("si 1111,aa")]
    [InlineData("ky1-1111,aa")]
    [InlineData("ky1 1111,aa")]
    [InlineData("ky11111,aa")]
    [InlineData("lv-1111,aa")]
    [InlineData("lv 1111,aa")]
    [InlineData("lv1111,aa")]
    [InlineData("md-1111,aa")]
    [InlineData("md 1111,aa")]
    [InlineData("md1111,aa")]
    [InlineData("MSR 1110-1350,aa")]
    [InlineData("MSR1110-1350,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPEndsWithFourNumbersAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPEndsWithFourNumbersAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("1111 a")]
    [InlineData("1111a")]
    [InlineData("1111 aa")]
    [InlineData("1111aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPStartsWithFourNumbers_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPStartsWithFourNumbers, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("1111 a,aa")]
    [InlineData("1111a,aa")]
    [InlineData("1111 aa,aa")]
    [InlineData("1111aa,aa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPStartsWithFourNumbersAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPStartsWithFourNumbersAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("je11bb")]
    [InlineData("je1 1bb")]
    [InlineData("bb11bb")]
    [InlineData("bb1 1bb")]
    [InlineData("bb111bb")]
    [InlineData("bb11 1bb")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPMiddleNumbers_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPMiddleNumbers, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("je11bb,bb")]
    [InlineData("je1 1bb,bb")]
    [InlineData("bb11bb,bb")]
    [InlineData("bb1 1bb,bb")]
    [InlineData("bb111bb,bb")]
    [InlineData("bb11 1bb,bb")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPMiddleNumbersAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPMiddleNumbersAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("A0A 0A0")]
    [InlineData("B1N 0A0")]
    [InlineData("J9P 0A0")]
    [InlineData("N1P 0A0")]
    [InlineData("S6V 0A0")]
    [InlineData("V1C 0A0")]
    [InlineData("X0A 0A0")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPCanadaPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPCanadaPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("A0A 0A0,CA")]
    [InlineData("B1N 0A0,CA")]
    [InlineData("J9P 0A0,CA")]
    [InlineData("N1P 0A0,CA")]
    [InlineData("S6V 0A0,CA")]
    [InlineData("V1C 0A0,CA")]
    [InlineData("X0A 0A0,CA")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPCanadaPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPCanadaPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("SW1W 0NY")]
    [InlineData("L1 8JQ")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPGBPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPGBPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("SW1W 0NY,GB")]
    [InlineData("L1 8JQ,GB")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPGBPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPGBPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("a1111aaa")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPArgentinaPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPArgentinaPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("a1111aaa,ar")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPArgentinaPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPArgentinaPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("bb 11")]
    [InlineData("bb bb")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPBermudaPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPBermudaPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("bb 11,bm")]
    [InlineData("bb bb,bm")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPBermudaPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPBermudaPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("GX11 1AA")]
    [InlineData("GX111AA")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPGibraltarPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPGibraltarPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("GX11 1AA,gi")]
    [InlineData("GX111AA,gi")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPGibraltarPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPGibraltarPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("LT-11111")]
    [InlineData("LT 11111")]
    [InlineData("LT11111")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPLithuaniaPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPLithuaniaPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("LT-11111,lt")]
    [InlineData("LT 11111,lt")]
    [InlineData("LT11111,lt")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPLithuaniaPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPLithuaniaPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("b111")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPSwazilandPostalCode_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPSwazilandPostalCode, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("b111,sz")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeZIPSwazilandPostalCodeAndCountry_IsAddressFalse(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ZIPSwazilandPostalCodeAndCountry, locationTypeResult);
        Assert.False(IsAddressResult);
    }

    [Theory]
    [InlineData("us")]
    [InlineData("mn,us")]
    [InlineData("minnesota, mn")]
    [InlineData("minnesota, mn, us")]
    [InlineData("united states")]
    [InlineData("london")]
    public void Given_GetLocationTypeFromString_ReturnsLocationTypeADDRESS_IsAddressTrue(string query)
    {
        // Arrange
        _output.WriteLine(query);
        var (locationTypeResult, IsAddressResult) = _locationStringMatches.GetLocationTypeFromString(query);
        //_output.WriteLine(locationTypeResult);
        //_output.WriteLine($"IsAddress: {IsAddressResult}");

        /// Assert
        Assert.Equal(LocationType.ADDRESS, locationTypeResult);
        Assert.True(IsAddressResult);
    }
}
