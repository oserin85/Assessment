using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GenerateConsumerConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                  .AddEnvironmentVariables()
                  .Build();
        }

        public static T GetConfiguration<T>(this IConfiguration config, string key)
        {
            var settings = (T)Activator.CreateInstance(typeof(T));
            config.Bind(key, settings);
            return settings;
        }

    }
}
