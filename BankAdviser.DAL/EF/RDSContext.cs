using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Services;
using System;
using System.Collections.Generic;
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate1Months = 0.09,
                Rate3Months = 0.105,
                Rate6Months = 0.12,
                Rate12Months = 0.11,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate1Months = 0.01,
                Rate3Months = 0.0175,
                Rate6Months = 0.02,
                Rate12Months = 0.0225,                
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 0.01,
                Rate6Months = 0.01,
                Rate12Months = 0.01,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 0.1375,
                Rate6Months = 0.145,
                Rate12Months = 0.13,
                Rate18Months = 0.10,
                Rate24Months = 0.10,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 0.025,
                Rate6Months = 0.0275,
                Rate12Months = 0.03,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.01,
                Rate6Months = 0.0125,
                Rate12Months = 0.0125,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 0.07,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 0.01,
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
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate12Months = 0.005,
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
                MaxSum = 200_000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,                
                Rate3Months = 0.128,
                Rate6Months = 0.142,
                Rate9Months = 0.142,
                Rate12Months = 0.142,
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
                MinSum = 200_000,               
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.131,
                Rate6Months = 0.145,
                Rate9Months = 0.145,
                Rate12Months = 0.145,
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
                MaxSum = 200_000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.10,
                Rate3Months = 0.133,
                Rate6Months = 0.147,
                Rate9Months = 0.147,
                Rate12Months = 0.147,
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
                MinSum = 200_000,               
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.103,
                Rate3Months = 0.136,
                Rate6Months = 0.15,
                Rate9Months = 0.15,
                Rate12Months = 0.15,
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
                MaxSum = 20_000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.016,
                Rate6Months = 0.024,
                Rate9Months = 0.025,
                Rate12Months = 0.027,
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
                MinSum = 20_000,               
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.017,
                Rate6Months = 0.025,
                Rate9Months = 0.026,
                Rate12Months = 0.028,
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
                MaxSum = 20_000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.008,
                Rate3Months = 0.019,
                Rate6Months = 0.027,
                Rate9Months = 0.028,
                Rate12Months = 0.03,
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
                MinSum = 20_000,                
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.009,
                Rate3Months = 0.02,
                Rate6Months = 0.028,
                Rate9Months = 0.029,
                Rate12Months = 0.031,
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
                MaxSum = 20_000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.004,
                Rate6Months = 0.009,
                Rate9Months = 0.009,
                Rate12Months = 0.009,
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
                MinSum = 20_000,               
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.005,
                Rate6Months = 0.01,
                Rate9Months = 0.01,
                Rate12Months = 0.01,
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
                MaxSum = 20_000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.0011,
                Rate3Months = 0.007,
                Rate6Months = 0.012,
                Rate9Months = 0.012,
                Rate12Months = 0.012,
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
                MinSum = 20_000,               
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.002,
                Rate3Months = 0.008,
                Rate6Months = 0.013,
                Rate9Months = 0.013,
                Rate12Months = 0.013,
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
                MaxSum = 100_000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.0605,
                Rate2Months = 0.0605,
                Rate3Months = 0.0815,
                Rate6Months = 0.0925,
                Rate12Months = 0.0975,
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
                MinSum = 100_000,               
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.063,
                Rate2Months = 0.063,
                Rate3Months = 0.084,
                Rate6Months = 0.095,
                Rate12Months = 0.10,
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
                MaxSum = 10_000,
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.001,
                Rate2Months = 0.001,
                Rate3Months = 0.001,
                Rate6Months = 0.001,
                Rate12Months = 0.0015,
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
                MinSum = 10_000,                
                InterestsPeriodicity = InterestsPeriodicity.OnCompletion,
                Rate1Months = 0.001,
                Rate2Months = 0.001,
                Rate3Months = 0.001,
                Rate6Months = 0.001,
                Rate12Months = 0.0015,
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
                MaxSum = 100_000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.0715,
                Rate6Months = 0.0825,
                Rate12Months = 0.0875,
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
                MinSum = 100_000,                
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.074,
                Rate6Months = 0.085,
                Rate12Months = 0.09,
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
                MaxSum = 10_000,
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.001,
                Rate6Months = 0.001,
                Rate12Months = 0.001,
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
                MinSum = 10_000,                
                InterestsPeriodicity = InterestsPeriodicity.Monthly,
                Rate3Months = 0.001,
                Rate6Months = 0.001,
                Rate12Months = 0.001,
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