namespace WeatherAPI.Services;
public sealed class GeneralHTTPService
{

    private readonly HttpClient _client;
    //private readonly string _baseURL = "";

    public GeneralHTTPService(HttpClient client)
    {
        _client = client;
        ConfigureClient();
    }

    public async Task<HttpResponseMessage> GetURLAsync(string url)
    {
        var content = await _client.GetAsync(url);

        return content;
    }

    private void ConfigureClient()
    {
        //_client.DefaultRequestHeaders.Add("Authorization", tomorrowSettings.AccessToken)
        //_client.DefaultRequestHeaders.Add("User-Agent", tomorrowSettings.UserAgent)
        //_client.BaseAddress = new Uri(_baseURL);
    }
}
