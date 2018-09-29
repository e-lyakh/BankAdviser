using System;
using System.Configuration;

namespace BankAdviser.BLL.Infrastructure
{
    public static class BotSettings
    {
        private const int waitPageToLoad = 1;
        private const int maxWaitElementToLoad = 15;

        public static int WaitPageToLoad
        {
            get { return waitPageToLoad; }
        }
        public static int MaxWaitElementToLoad
        {
            get { return maxWaitElementToLoad; }
        }

        public static bool IsBrowserMinimized
        {
            get
            {
                return Convert.ToBoolean(Read("IsBrowserMinimized"));
            }
            set
            {
                AddUpdate("IsBrowserMinimized", value.ToString());
            }
        }

        private static string Read(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string value = appSettings[key] ?? null;

                if (value == null)
                    throw new Exception($"Settings are not found (key = {key})");

                return value;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
            catch (Exception ex)
            {
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

            }
        }
    }
}