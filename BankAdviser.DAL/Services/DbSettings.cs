using System;
using System.Configuration;
using System.Data.Common;

namespace BankAdviser.DAL.Services
{
    public static class DbSettings
    {
        private const string hostName = "bankadviserdb.cgcys6pol5qy.eu-central-1.rds.amazonaws.com";
        private const string dbName = "bankadviserdb";
        private const string userName = "ev_lyakh";
        private const string password = "IT-Step_Diploma2018";
        private const string port = "1433";

        private const string connectionString = "Data Source=" + hostName +
                                                ";Initial Catalog=" + dbName +
                                                ";User ID=" + userName +
                                                ";Password=" + password +
                                                ";MultipleActiveResultSets=True;";

        private const string providerName = "System.Data.SqlClient";

        public static DbConnection DbConnection
        {
            get
            {
                return GetDbConnection();
            }
        }

        private static DbConnection GetDbConnection()
        {
            DbConnection dbCon = DbProviderFactories.GetFactory(providerName).CreateConnection();
            dbCon.ConnectionString = connectionString;
            return dbCon;
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