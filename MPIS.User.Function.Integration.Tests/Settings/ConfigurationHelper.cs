#region "Libraries"

using Microsoft.Extensions.Configuration;

#endregion

namespace MPIS.User.Function.Integration.Tests.Settings
{
    public static class ConfigurationHelper
    {
        #region "Atributes"

        private static Settings _settings;

        #endregion "Atributes"

        #region "Properties"

        public static Settings Settings
        {
            get
            {
                if (_settings != null)
                {
                    return _settings;
                }


                var configurationRoot = new ConfigurationBuilder()
                    .AddJsonFile("local.integration.settings.json")
                    .AddEnvironmentVariables()
                    .Build();
                _settings = new Settings();
                configurationRoot.Bind(_settings);

                return _settings;
            }
        }

        #endregion "Properties"
    }
}
