using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Services;
using System.Configuration;
using System.Data.Entity;

namespace BankAdviser.DAL.EF
{
    public class RDSContext : DbContext
    {
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<DepositInfo> Searches { get; set; }
        public DbSet<Reply> Responses { get; set; }

        static RDSContext()
        {
            Database.SetInitializer<RDSContext>(new StoreDbInitializer());
        }

        public RDSContext() : base(GetRDSConnectionString())
        { }
        

        public static string GetRDSConnectionString()
        {
            //var appConfig = ConfigurationManager.AppSettings;

            ////string dbname = appConfig["RDS_DB_NAME"];
            //string dbname = Settings.DbName;

            //if (string.IsNullOrEmpty(dbname)) return null;

            //string username = appConfig["RDS_USERNAME"];
            //string password = appConfig["RDS_PASSWORD"];
            //string hostname = appConfig["RDS_HOSTNAME"];
            //string port = appConfig["RDS_PORT"];

            //return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
            return "Data Source=bankadviserdb.cgcys6pol5qy.eu-central-1.rds.amazonaws.com;Initial Catalog=bankadviserdb;User ID=ev_lyakh;Password=IT-Step_Diploma2018;";
        }
    }    

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<RDSContext>
    {
        protected override void Seed(RDSContext db)
        {
            db.Dialogs.Add(new Dialog
            {
                Id = 0,
                Language = "Rus",
                Sum = "Укажите сумму для вложения:",
                Currency = "Укажите валюту:",
                InterestPayout = "Когда выплачивать проценты?:",
                BanksGroup = "Выберите группу банков:",
                Term = "Выберите период вложения:",
                BanksNum = "Количество банков:",
                Email = "Укажите e-mail для отправки информации:",
                Format = "Укажите формат отправки информации:"
            });
            db.Dialogs.Add(new Dialog
            {
                Id = 1,
                Language = "Укр",
                Sum = "Вкажіть суму до вкладення:",
                Currency = "Вкажіть валюту:",
                InterestPayout = "Коли виплачувати відсотки?:",
                BanksGroup = "Выберіть групу банків:",
                Term = "Выберіть період вкладення:",
                BanksNum = "Кількість банків:",
                Email = "Вкажіть e-mail для відправки информації:",
                Format = "Вкажіть формат відправки информації:"
            });
            db.Dialogs.Add(new Dialog
            {
                Id = 2,
                Language = "Eng",
                Sum = "Choose a deposit sum:",
                Currency = "Choose currency:",
                InterestPayout = "When should interests be paid?:",
                BanksGroup = "Choose a bank group:",
                Term = "Choose a deposit term:",
                BanksNum = "Number of banks:",
                Email = "E-mail to send information:",
                Format = "Information format:"
            });

            db.SaveChanges();
        }
    }
}