#region "Libraries"

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

#endregion

namespace MPIS.User.Function.Configuration
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Add configuration as singleton (like MVC)
        /// Recommended: https://stackoverflow.com/a/56080877/9366419
        /// </summary>
        /// <param name="builder"></param>
        public static void AddConfiguration(this IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddMvcCore()
                     .AddJsonFormatters()
                     .AddJsonOptions(options =>
                     {
                         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     });

            builder.Services.AddSingleton(configuration);
            builder.Services.AddSingleton<SystemConfiguration>();

        }
    }
}
