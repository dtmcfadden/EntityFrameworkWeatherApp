# EntityFrameworkWeatherApp

### Purpose

- Connect to multiple weather APIs
- Use **Swagger** to test and interact with API
- Test using **XUnit** and **Mock**
- Use Model View Controller (**MVC**) for API and web page routes
- Have **Blazor** components for reactive web pages
- Use **Mediatr** to implement the Mediator pattern to implement **CQRS**
- Use **FluentValidation** to check API input and classes

### Setup

1. This program interacts with 3rd party weather APIs that need to be applied for to get their API keys.
   1. [Open Weather](https://openweathermap.org/)
      - [Signup](https://home.openweathermap.org/users/sign_up)
      - [API Documentation](https://openweathermap.org/current)
   2. [Weather API](https://www.weatherapi.com/)
      - [Signup](https://www.weatherapi.com/signup.aspx)
      - [API Documentation](https://www.weatherapi.com/docs/)
2. Use the API Keys and setup user secrets in the program
   1. Directions for setting up user secrets
      - [Microsoft directions](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#manage-user-secrets-with-visual-studio)
      - Esentially right click on project and select "Manage Use Secrets"
   2. The secrets are mapped to classes for ease of use in the code
   3. Inlude the secrets in
      - WeatherAPI - Used for the program that will be connecting to the 3rd party APIs
      - WeatherAPI.DevTests - Used for testing purposes
   4. The Secrets should follow the below structure
   ```json
   {
   	"OpenWeather": {
   		"APIKey": "Insert Key Here"
   	},
   	"WeatherAPI": {
   		"APIKey": "Insert Key Here"
   	}
   }
   ```

### Project Descriptions

<dl>
	<dt><b>EntityFrameworkWeatherApp</b></dt>
	<dd>Main project. Has MVC logic.</dd>
	<dt><b>EntityFrameworkWeatherApp.Abstractions</b></dt>
	<dd>Classes used accross projects.</dd>
	<dt><b>WeatherAPI</b></dt>
	<dd>Logic used to connect to the weather APIs</dd>
	<dt><b>EntityFrameworkWeatherApp.Abstractions.UnitTests</b></dt>
	<dd>Unit tests.</dd>
	<dt><b>WeatherAPI.DevTests</b></dt>
	<dd>Tests used in development but not meant to be run in the CI/CD pipeline.</dd>
	<dt><b>WeatherAPI.UnitTests</b></dt>
	<dd>Tests meant to be run in the CI/CD pipeline for the WeatherAPI project.</dd>
</dl>
