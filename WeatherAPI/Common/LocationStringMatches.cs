using System.Text.RegularExpressions;

namespace WeatherAPI.Common;
public class LocationStringMatches
{
    public string NumberStartsWithNine { get; } = @"9[7-8]{1}[0-9]{3}";
    public string NumberWithOptionalDashMask { get; } = @"\d{2,5}(?:(?:-\d{3,6})?|(?:\d{1})?)";
    public string NumberWithParenthesis { get; } = @"\d{5}(?:(?:\s\(\d{3}\s)|(?:\d{1}\s\(\d{3}-\d{1})|(?:\d{2}\s\(\d{3}-\d{2}))\d{2}\)";
    public string LettersWith1ZZ { get; } = @"[abfpst]{1}[a-z]{1}(?:(?:[c]{1}[anu]{1})|(?:[q]{2})|(?:[n]{1}[d]{1})|(?:[r]{1}[n]{1})|(?:[h]{1}[l]{1}))\s{0,1}[1][z]{2}";
    public string StartsWithAHasNumbers { get; } = @"a(?:(?:d\d{3})|(?:z\d{4,5})|(?:i-2640))";
    public string NumberWithSpace { get; } = @"\d{3}(?:(?:\s\d{2,3})|(?:\d{1}\s\d{3,4}))";
    public string EndsWithFourNumbers { get; } = @"(?:(?:[a-z]{2})|(?:(?:si|md|lv|ky\d|[a-z]{3})[- ]{0,1})|(?:msr\s{0,1}\d{4}-))\d{4}";
    public string StartsWithFourNumbers { get; } = @"\d{4}\s{0,1}[a-z]{1,2}";
    public string MiddleNumbers { get; } = @"(?:(?:je)|(?:[a-z]{2}\d{0,1}))\d\s{0,1}\d[a-z]{2}";
    public string CanadaPostalCode { get; } = @"(?=[^dfioqu\d\s])[a-z]\d(?=[^dfioqu\d\s])[a-z]\s{0,1}\d(?=[^dfioqu\d\s])[a-z]\d";
    public string GBPostalCode { get; } = @"[a-z]{1,2}[0-9R][0-9a-z]?\s{0,1}[0-9][a-z-[CIKMOV]]{2}";
    public string ArgentinaPostalCode { get; } = @"[a-z]\d{4}[a-z]{3}";
    public string BermudaPostalCode { get; } = @"[a-z]{2}\s([a-z]{2}|\d{2})";
    public string GibraltarPostalCode { get; } = @"[g][x][1]{2}\s{0,1}[1][a]{2}";
    public string LithuaniaPostalCode { get; } = @"[l][t][- ]{0,1}\d{5}";
    public string SwazilandPostalCode { get; } = @"[a-z]\d{3}";

    //public string CommaTwoString { get; } = @"(?:,[a-z]{2})";
    public string CommaTwoString { get; } = @"(?:,(?!ca|gb|ar|bm|gi|lt|sz)[a-z]{2})";
    // ,(?!ca|gb|ar|bm|gi|lt|sz)[a-z]{2}

    private readonly RegexOptions regexOptions = RegexOptions.IgnoreCase;

    public enum LocationType
    {
        ADDRESS,
        ZIPNumberStatsWithNine, ZIPNumberStatsWithNineAndCountry,
        ZIPNumberWithDash, ZIPNumberWithDashAndCountry,
        ZIPNumberWithParenthesis, ZIPNumberWithParenthesisAndCountry,
        ZIPLettersWith1ZZ, ZIPLettersWith1ZZAndCountry,
        ZIPStartsWithAHasNumbers, ZIPStartsWithAHasNumbersAndCountry,
        ZIPNumberWithSpace, ZIPNumberWithSpaceAndCountry,
        ZIPEndsWithFourNumbers, ZIPEndsWithFourNumbersAndCountry,
        ZIPStartsWithFourNumbers, ZIPStartsWithFourNumbersAndCountry,
        ZIPMiddleNumbers, ZIPMiddleNumbersAndCountry,
        ZIPCanadaPostalCode, ZIPCanadaPostalCodeAndCountry,
        ZIPGBPostalCode, ZIPGBPostalCodeAndCountry,
        ZIPArgentinaPostalCode, ZIPArgentinaPostalCodeAndCountry,
        ZIPBermudaPostalCode, ZIPBermudaPostalCodeAndCountry,
        ZIPGibraltarPostalCode, ZIPGibraltarPostalCodeAndCountry,
        ZIPLithuaniaPostalCode, ZIPLithuaniaPostalCodeAndCountry,
        ZIPSwazilandPostalCode, ZIPSwazilandPostalCodeAndCountry
    };

    public (LocationType locationType, bool IsAddress) GetLocationTypeFromString(string query)
    {
        return query switch
        {
            string s when Regex.IsMatch(s, @$"^{GibraltarPostalCode}$", regexOptions) => (LocationType.ZIPGibraltarPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{GibraltarPostalCode}(?:,gi)$", regexOptions) => (LocationType.ZIPGibraltarPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{NumberStartsWithNine}$", regexOptions) => (LocationType.ZIPNumberStatsWithNine, false),
            string s when Regex.IsMatch(s, @$"^{NumberStartsWithNine}{CommaTwoString}$", regexOptions) => (LocationType.ZIPNumberStatsWithNineAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithOptionalDashMask}$", regexOptions) => (LocationType.ZIPNumberWithDash, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithOptionalDashMask}{CommaTwoString}$", regexOptions) => (LocationType.ZIPNumberWithDashAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithParenthesis}$", regexOptions) => (LocationType.ZIPNumberWithParenthesis, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithParenthesis}{CommaTwoString}$", regexOptions) => (LocationType.ZIPNumberWithParenthesisAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{LettersWith1ZZ}$", regexOptions) => (LocationType.ZIPLettersWith1ZZ, false),
            string s when Regex.IsMatch(s, @$"^{LettersWith1ZZ}{CommaTwoString}$", regexOptions) => (LocationType.ZIPLettersWith1ZZAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{StartsWithAHasNumbers}$", regexOptions) => (LocationType.ZIPStartsWithAHasNumbers, false),
            string s when Regex.IsMatch(s, @$"^{StartsWithAHasNumbers}{CommaTwoString}$", regexOptions) => (LocationType.ZIPStartsWithAHasNumbersAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithSpace}$", regexOptions) => (LocationType.ZIPNumberWithSpace, false),
            string s when Regex.IsMatch(s, @$"^{NumberWithSpace}{CommaTwoString}$", regexOptions) => (LocationType.ZIPNumberWithSpaceAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{EndsWithFourNumbers}$", regexOptions) => (LocationType.ZIPEndsWithFourNumbers, false),
            string s when Regex.IsMatch(s, @$"^{EndsWithFourNumbers}{CommaTwoString}$", regexOptions) => (LocationType.ZIPEndsWithFourNumbersAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{StartsWithFourNumbers}$", regexOptions) => (LocationType.ZIPStartsWithFourNumbers, false),
            string s when Regex.IsMatch(s, @$"^{StartsWithFourNumbers}{CommaTwoString}$", regexOptions) => (LocationType.ZIPStartsWithFourNumbersAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{MiddleNumbers}$", regexOptions) => (LocationType.ZIPMiddleNumbers, false),
            string s when Regex.IsMatch(s, @$"^{MiddleNumbers}{CommaTwoString}$", regexOptions) => (LocationType.ZIPMiddleNumbersAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{CanadaPostalCode}$", regexOptions) => (LocationType.ZIPCanadaPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{CanadaPostalCode}(?:,ca)$", regexOptions) => (LocationType.ZIPCanadaPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{GBPostalCode}$", regexOptions) => (LocationType.ZIPGBPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{GBPostalCode}(?:,gb)$", regexOptions) => (LocationType.ZIPGBPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{ArgentinaPostalCode}$", regexOptions) => (LocationType.ZIPArgentinaPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{ArgentinaPostalCode}(?:,ar)$", regexOptions) => (LocationType.ZIPArgentinaPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{BermudaPostalCode}$", regexOptions) => (LocationType.ZIPBermudaPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{BermudaPostalCode}(?:,bm)$", regexOptions) => (LocationType.ZIPBermudaPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{LithuaniaPostalCode}$", regexOptions) => (LocationType.ZIPLithuaniaPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{LithuaniaPostalCode}(?:,lt)$", regexOptions) => (LocationType.ZIPLithuaniaPostalCodeAndCountry, false),
            string s when Regex.IsMatch(s, @$"^{SwazilandPostalCode}$", regexOptions) => (LocationType.ZIPSwazilandPostalCode, false),
            string s when Regex.IsMatch(s, @$"^{SwazilandPostalCode}(?:,sz)$", regexOptions) => (LocationType.ZIPSwazilandPostalCodeAndCountry, false),
            _ => (LocationType.ADDRESS, true),
        };
    }
}
