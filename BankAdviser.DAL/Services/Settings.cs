using System;
using System.Configuration;

namespace BankAdviser.DAL.Services
{
    public static class Settings
    {        
        private static readonly string hostName = Read("RDS_HOSTNAME");
        private static readonly string dbName = Read("RDS_DBNAME");
        private static readonly string userName = Read("RDS_USERNAME");
        private static readonly string password = Read("RDS_PASSWORD");
        private static readonly string port = Read("RDS_PORT");        

        public static string ConnectionString
        {
            get
            {
                return "Data Source=" + hostName + ";Initial Catalog=" + dbName + ";User ID=" + userName + ";Password=" + password + ";MultipleActiveResultSets=True;";
            }
        }

        public static double WaitElement { get; } = Convert.ToDouble(Read("WaitElementToLoad"));

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
                // TODO: exception logging
                return null;
            }
            catch (Exception ex)
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