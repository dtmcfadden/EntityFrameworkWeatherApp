using System.Collections;

namespace EntityFrameworkWeatherApp.Abstractions.Common;
public static class EnvironmentMethods
{
    public static bool IsDevelopment { get; set; }

    public static IDictionary? GetEnvironmentVariables(string target = "machine")
    {
        return Environment.GetEnvironmentVariables(EnvironmentTarget(target));
    }

    public static string? GetEnvironmentVariable(string name, string target = "machine")
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentTarget(target));
    }

    private static EnvironmentVariableTarget EnvironmentTarget(string name)
    {
        return name.ToLower() switch
        {
            "process" => EnvironmentVariableTarget.Process,
            "user" => EnvironmentVariableTarget.User,
            "machine" => EnvironmentVariableTarget.Machine,
            _ => default
        };
    }
}
