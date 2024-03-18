﻿global using EntityFrameworkWeatherApp.Abstractions.Results;
global using MediatR;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Moq;
global using System.Text.Json;
global using WeatherAPI.DevTests.Requests.Queries.OpenWeather;
global using WeatherAPI.DevTests.TestFactories;
global using WeatherAPI.Entities;
global using WeatherAPI.Entities.Validators;
global using WeatherAPI.Models.OpenWeather;
global using WeatherAPI.Models.WeatherAPI;
global using WeatherAPI.Options;
global using WeatherAPI.Requests.Queries.OpenWeather;
global using WeatherAPI.Requests.Queries.WeatherAPI;
global using WeatherAPI.Services;
global using Xunit.Abstractions;
global using static WeatherAPI.Common.LocationStringMatches;
