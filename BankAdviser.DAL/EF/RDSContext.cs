using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Services;
using System;
using System.Data.Entity;

namespace BankAdviser.DAL.EF
{
    public class RDSContext : DbContext
    {       
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }        
        public DbSet<ReplyEntry> ReplyEntries { get; set; }

        static RDSContext()
        {
            Database.SetInitializer<RDSContext>(new StoreDbInitializer());
        }

        public RDSContext() : base(Settings.ConnectionString)
        {
        }

        public RDSContext(string connectionStr) : base(connectionStr)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<RDSContext>
    {
        protected override void Seed(RDSContext db)
        {
            #region Banks

            db.Banks.Add(new Bank
            {
                Id = 1,
                SearchDate = DateTime.Now,
                Name = "ПриватБанк",
                Type = BankType.State,
                AssetsRank = 1,
                Rating = 10
            });
            db.Banks.Add(new Bank
            {
                Id = 2,
                SearchDate = DateTime.Now,
                Name = "Ощадбанк",
                Type = BankType.State,
                AssetsRank = 2,
                Rating = 8
            });            
            db.Banks.Add(new Bank
            {
                Id = 3,
                SearchDate = DateTime.Now,
                Name = "ПУМБ",
                Type = BankType.Private,
                AssetsRank = 7,
                Rating = 12
            });
            db.Banks.Add(new Bank
            {
                Id = 4,
                SearchDate = DateTime.Now,
                Name = "Пивденный",
                Type = BankType.Private,
                AssetsRank = 16,
                Rating = 16
            });
            db.Banks.Add(new Bank
            {
                Id = 5,
                SearchDate = DateTime.Now,
                Name = "Райффайзен Банк Аваль",
                Type = BankType.Foreign,
                AssetsRank = 5,
                Rating = 1
            });
            db.Banks.Add(new Bank
            {
                Id = 6,
                SearchDate = DateTime.Now,
                Name = "Credit Agricole",
                Type = BankType.Foreign,
                AssetsRank = 13,
                Rating = 3
            });
            #endregion
            
            #region Deposits

            // PrivatBank
            db.Deposits.Add(new Deposit
            {
                Id = 1,
                SearchDate = DateTime.Now,
                BankId = 2,
                Name = "Стандарт",
                Currency = Currency.UAH,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate1Months = 09,
                Rate3Months = 10.5,
                Rate6Months = 12,
                Rate12Months = 11,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,                
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });            
            db.Deposits.Add(new Deposit
            {
                Id = 2,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Стандарт",
                Currency = Currency.USD,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate1Months = 1,
                Rate3Months = 1.75,
                Rate6Months = 2,
                Rate12Months = 2.25,                
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });            
            db.Deposits.Add(new Deposit
            {
                Id = 3,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Стандарт",
                Currency = Currency.EUR,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 1,
                Rate6Months = 1,
                Rate12Months = 1,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });

            db.Deposits.Add(new Deposit
            {
                Id = 4,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Стандарт срочный",
                Currency = Currency.UAH,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 13.75,
                Rate6Months = 14.5,
                Rate12Months = 13,
                Rate18Months = 10,
                Rate24Months = 10,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = false,                
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 5,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Стандарт срочный",
                Currency = Currency.USD,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 2.5,
                Rate6Months = 2.75,
                Rate12Months = 3,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });            
            db.Deposits.Add(new Deposit
            {
                Id = 6,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Стандарт срочный",
                Currency = Currency.EUR,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 1,
                Rate6Months = 1.25,
                Rate12Months = 1.25,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.5% при продлении",
                Url = "https://privatbank.ua/ru/depozit"
            });

            db.Deposits.Add(new Deposit
            {
                Id = 7,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Приват-вклад",
                Currency = Currency.UAH,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 7,
                IsAddable = true,
                IsWithdrawable = true,
                IsCancellable = true,                
                Url = "https://privatbank.ua/ru/depozit"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 8,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Приват-вклад",
                Currency = Currency.USD,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 1,
                IsAddable = true,
                IsWithdrawable = true,
                IsCancellable = true,
                Url = "https://privatbank.ua/ru/depozit"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 9,
                SearchDate = DateTime.Now,
                BankId = 1,
                Name = "Приват-вклад",
                Currency = Currency.EUR,
                MinSum = 0,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 0.5,
                IsAddable = true,
                IsWithdrawable = true,
                IsCancellable = true,
                Url = "https://privatbank.ua/ru/depozit"
            });

            // PUMB
            db.Deposits.Add(new Deposit
            {
                Id = 10,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.UAH,
                MinSum = 2500,
                MaxSum = 200000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 12.8,
                Rate6Months = 14.2,
                Rate9Months = 14.2,
                Rate12Months = 14.2,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,                
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 11,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.UAH,
                MinSum = 200000,                
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 13.1,
                Rate6Months = 14.5,
                Rate9Months = 14.5,
                Rate12Months = 14.5,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 12,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.UAH,
                MinSum = 2500,
                MaxSum = 200000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 10,
                Rate3Months = 13.3,
                Rate6Months = 14.7,
                Rate9Months = 14.7,
                Rate12Months = 14.7,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 13,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.UAH,
                MinSum = 200000,               
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 10.3,
                Rate3Months = 13.6,
                Rate6Months = 15,
                Rate9Months = 15,
                Rate12Months = 15,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 14,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.USD,               
                MinSum = 100,
                MaxSum = 20000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 1.6,
                Rate6Months = 2.4,
                Rate9Months = 2.5,
                Rate12Months = 2.7,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,                
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 15,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.USD,
                MinSum = 20000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 1.7,
                Rate6Months = 2.5,
                Rate9Months = 2.6,
                Rate12Months = 2.8,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 16,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.USD,
                MinSum = 100,
                MaxSum = 20000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.8,
                Rate3Months = 1.9,
                Rate6Months = 2.7,
                Rate9Months = 2.8,
                Rate12Months = 3,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 17,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.USD,
                MinSum = 20000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.9,
                Rate3Months = 2,
                Rate6Months = 2.8,
                Rate9Months = 2.9,
                Rate12Months = 3.1,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 18,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.EUR,                
                MinSum = 100,
                MaxSum = 20000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.4,
                Rate6Months = 0.9,
                Rate9Months = 0.9,
                Rate12Months = 0.9,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,                
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 19,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.EUR,
                MinSum = 20000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.5,
                Rate6Months = 1,
                Rate9Months = 1,
                Rate12Months = 1,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 20,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.EUR,
                MinSum = 100,
                MaxSum = 20000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.11,
                Rate3Months = 0.7,
                Rate6Months = 1.2,
                Rate9Months = 1.2,
                Rate12Months = 1.2,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 21,
                SearchDate = DateTime.Now,
                BankId = 3,
                Name = "Доходный",
                Currency = Currency.EUR,
                MinSum = 20000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.2,
                Rate3Months = 0.8,
                Rate6Months = 1.3,
                Rate9Months = 1.3,
                Rate12Months = 1.3,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = false,
                BonusInfo = "+0.3% при продлении, при размещении через ПУМБ Online",
                Url = "https://retail.pumb.ua/ru/deposit/profitable"
            });

            // Aval:
            db.Deposits.Add(new Deposit
            {
                Id = 22,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Классический",
                Currency = Currency.UAH,
                MinSum = 2000,
                MaxSum = 100000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 6.05,
                Rate2Months = 6.05,
                Rate3Months = 8.15,
                Rate6Months = 9.25,
                Rate12Months = 9.75,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = true,               
                Url = "https://www.aval.ua/ru/personal/accounts/vkladclassic/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 23,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Классический",
                Currency = Currency.UAH,
                MinSum = 100000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 6.3,
                Rate2Months = 6.3,
                Rate3Months = 8.4,
                Rate6Months = 9.5,
                Rate12Months = 10,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladclassic/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 24,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Классический",
                Currency = Currency.USD,
                MinSum = 500,
                MaxSum = 10000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.1,
                Rate2Months = 0.1,
                Rate3Months = 0.1,
                Rate6Months = 0.1,
                Rate12Months = 0.15,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladclassic/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 25,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Классический",
                Currency = Currency.USD,
                MinSum = 10000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.1,
                Rate2Months = 0.1,
                Rate3Months = 0.1,
                Rate6Months = 0.1,
                Rate12Months = 0.15,
                IsAddable = false,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladclassic/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 26,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Сберегательный",
                Currency = Currency.UAH,
                MinSum = 500,
                MaxSum = 100000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 7.15,
                Rate6Months = 8.25,
                Rate12Months = 8.75,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,                
                Url = "https://www.aval.ua/ru/personal/accounts/vkladoschadnij/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 27,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Сберегательный",
                Currency = Currency.UAH,
                MinSum = 100000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 7.4,
                Rate6Months = 8.5,
                Rate12Months = 9,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladoschadnij/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 28,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Сберегательный",
                Currency = Currency.USD,
                MinSum = 100,
                MaxSum = 10000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.1,
                Rate6Months = 0.1,
                Rate12Months = 0.1,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladoschadnij/"
            });
            db.Deposits.Add(new Deposit
            {
                Id = 29,
                SearchDate = DateTime.Now,
                BankId = 5,
                Name = "Сберегательный",
                Currency = Currency.USD,
                MinSum = 10000,
                MaxSum = 100000000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.1,
                Rate6Months = 0.1,
                Rate12Months = 0.1,
                IsAddable = true,
                IsWithdrawable = false,
                IsCancellable = true,
                Url = "https://www.aval.ua/ru/personal/accounts/vkladoschadnij/"
            });
            #endregion
            
            db.SaveChanges();
        }
    }
}