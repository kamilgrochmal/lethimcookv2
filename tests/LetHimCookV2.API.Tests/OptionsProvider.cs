﻿using LetHimCookV2.API.Common;
using Microsoft.Extensions.Configuration;

namespace LetHimCookV2.API.Tests;

public sealed class OptionsProvider
{
    private readonly IConfigurationRoot _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);
    
    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", true)
            .AddEnvironmentVariables()
            .Build();
}