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
        public DbSet<Rating> Ratings { get; set; }

        static RDSContext()
        {
            Database.SetInitializer<RDSContext>(new StoreDbInitializer());
        }

        public RDSContext() : base(DbSettings.DbConnection, true)
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
                Name = "Креди Агриколь",
                Type = BankType.Foreign,
                AssetsRank = 13,
                Rating = 3
            });
            db.Banks.Add(new Bank
            {
                Id = 7,
                SearchDate = DateTime.Now,
                Name = "Укрсиббанк",
                Type = BankType.Foreign,
                AssetsRank = 10,
                Rating = 2
            });
            #endregion

            #region Deposits

            // PrivatBank
            db.Deposits.Add(new Deposit
            {
                Id = 1,
                SearchDate = DateTime.Now,
                BankId = 1,
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

            #region Ratings

            db.Ratings.Add(new Rating
            {
                Id = 0,
                SearchDate = DateTime.Now,
                BankName = "Райффайзен Банк Аваль",
                OverallRating = 4.56,
                StressRating = 4.6,
                LoyaltyRating = 4.4,
                AnalystsRating = 4.82,
                DepositRating = 5,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 1,
                SearchDate = DateTime.Now,
                BankName = "Укрсиббанк",
                OverallRating = 4.5,
                StressRating = 4.2,
                LoyaltyRating = 4.7,
                AnalystsRating = 4.82,
                DepositRating = 8,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 2,
                SearchDate = DateTime.Now,
                BankName = "Креди Агриколь Банк",
                OverallRating = 4.35,
                StressRating = 4,
                LoyaltyRating = 4.5,
                AnalystsRating = 4.82,
                DepositRating = 12,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 3,
                SearchDate = DateTime.Now,
                BankName = "ОТП Банк",
                OverallRating = 4.26,
                StressRating = 4.1,
                LoyaltyRating = 4.4,
                AnalystsRating = 4.29,
                DepositRating = 9,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 4,
                SearchDate = DateTime.Now,
                BankName = "Кредобанк",
                OverallRating = 4.04,
                StressRating = 3.7,
                LoyaltyRating = 4.3,
                AnalystsRating = 4.29,
                DepositRating = 15,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 5,
                SearchDate = DateTime.Now,
                BankName = "Укргазбанк",
                OverallRating = 3.95,
                StressRating = 3.4,
                LoyaltyRating = 4.3,
                AnalystsRating = 4.47,
                DepositRating = 6,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 6,
                SearchDate = DateTime.Now,
                BankName = "ПроКредит Банк",
                OverallRating = 3.81,
                StressRating = 3.9,
                LoyaltyRating = 3.5,
                AnalystsRating = 4.02,
                DepositRating = 16,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 7,
                SearchDate = DateTime.Now,
                BankName = "Ощадбанк",
                OverallRating = 3.8,
                StressRating = 2.9,
                LoyaltyRating = 4.6,
                AnalystsRating = 4.38,
                DepositRating = 2,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 8,
                SearchDate = DateTime.Now,
                BankName = "Укрэксимбанк",
                OverallRating = 3.71,
                StressRating = 3.2,
                LoyaltyRating = 4.1,
                AnalystsRating = 4.2,
                DepositRating = 4,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 9,
                SearchDate = DateTime.Now,
                BankName = "ПриватБанк",
                OverallRating = 3.64,
                StressRating = 3.3,
                LoyaltyRating = 3.7,
                AnalystsRating = 4.2,
                DepositRating = 1,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 10,
                SearchDate = DateTime.Now,
                BankName = "Альфа-Банк",
                OverallRating = 3.6,
                StressRating = 2.4,
                LoyaltyRating = 4.8,
                AnalystsRating = 4.29,
                DepositRating = 3,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 11,
                SearchDate = DateTime.Now,
                BankName = "ПУМБ",
                OverallRating = 3.59,
                StressRating = 3.1,
                LoyaltyRating = 4.4,
                AnalystsRating = 3.22,
                DepositRating = 7,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 12,
                SearchDate = DateTime.Now,
                BankName = "Правэкс банк",
                OverallRating = 3.58,
                StressRating = 3.6,
                LoyaltyRating = 3.3,
                AnalystsRating = 4.11,
                DepositRating = 30,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 13,
                SearchDate = DateTime.Now,
                BankName = "Идея Банк",
                OverallRating = 3.5,
                StressRating = 3.3,
                LoyaltyRating = 3.8,
                AnalystsRating = 3.4,
                DepositRating = 23,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 14,
                SearchDate = DateTime.Now,
                BankName = "Укрсоцбанк",
                OverallRating = 3.4,
                StressRating = 3.5,
                LoyaltyRating = 3,
                AnalystsRating = 3.84,
                DepositRating = 13,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 15,
                SearchDate = DateTime.Now,
                BankName = "Банк Пивденный",
                OverallRating = 3.32,
                StressRating = 2.7,
                LoyaltyRating = 4.4,
                AnalystsRating = 2.78,
                DepositRating = 10,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 16,
                SearchDate = DateTime.Now,
                BankName = "Универсал Банк",
                OverallRating = 3.27,
                StressRating = 2.6,
                LoyaltyRating = 4.1,
                AnalystsRating = 3.31,
                DepositRating = 21,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 17,
                SearchDate = DateTime.Now,
                BankName = "Мегабанк",
                OverallRating = 3.26,
                StressRating = 2.5,
                LoyaltyRating = 4.4,
                AnalystsRating = 3.04,
                DepositRating = 18,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 18,
                SearchDate = DateTime.Now,
                BankName = "Сбербанк (России)",
                OverallRating = 3.13,
                StressRating = 3.1,
                LoyaltyRating = 3.7,
                AnalystsRating = 2.07,
                DepositRating = 11,
                Year = 2018,
                Quarter = 2
            });
            db.Ratings.Add(new Rating
            {
                Id = 19,
                SearchDate = DateTime.Now,
                BankName = "Таскомбанк",
                OverallRating = 3.04,
                StressRating = 2.1,
                LoyaltyRating = 4.1,
                AnalystsRating = 3.22,
                DepositRating = 14,
                Year = 2018,
                Quarter = 2
            });

            #endregion

            db.SaveChanges();
        }
    }
}