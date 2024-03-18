
using Asp.Versioning.ApiExplorer;
using EntityFrameworkWeatherApp;
using EntityFrameworkWeatherApp.Components;
using EntityFrameworkWeatherApp.Controllers.API;
using EntityFrameworkWeatherApp.OpenAPI;
using WeatherAPI;
using ConfigurationSettings = EntityFrameworkWeatherApp.ConfigurationSettings;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = ConfigurationSettings.GetConfigurationSettings();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

        builder.Services.AddSwaggerGen();

        // Add services to the container.
        builder.Services.AddSingleton(configuration);

        builder.Services.AddAppServices();
        builder.Services.AddAppWeatherAPIServices(builder.Configuration);

        var app = builder.Build();

        app.UseExceptionHandler();

        app.MapOpenWeatherEndpoints();
        app.MapWeatherAPIEndpoints();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

            foreach (ApiVersionDescription desc in descriptions.Reverse())
            {
                string url = $"/swagger/{desc.GroupName}/swagger.json";
                string name = $"{desc.GroupName.ToUpperInvariant()} {app.Environment.EnvironmentName}{(desc.IsDeprecated == true ? " Deprecated" : "")}";

                options.SwaggerEndpoint(url, name);
            }
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllerRoute(
            name: "default",
        //pattern: "{controller=weather}/{action=Index}/{id?}");
        pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}