using System.Configuration;

namespace BankAdviser.DAL.Services
{
    public static class Settings
    {
        public static string DbName { get; } = Read("RDS_DB_NAME");

        private static string Read(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string value = appSettings[key] ?? "not found";

                return value;
            }
            catch (ConfigurationErrorsException)
            {
                // TODO: exception logging
                return null;
            }
        }

        private static void AddUpdate(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                // TODO: exception logging
            }
        }
    }
}
