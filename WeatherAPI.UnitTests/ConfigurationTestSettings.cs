﻿using System.Reflection;

namespace WeatherAPI.UnitTests;
public static partial class ConfigurationTestSettings
{
    /// <summary>
    /// Gets settings
    /// </summary>
    public static IConfigurationRoot GetConfigurationSettings()
    {
        var returnConfiguration = new ConfigurationBuilder()
             .AddUserSecrets(Assembly.GetExecutingAssembly())
             .Build();

        return returnConfiguration;
    }
}
