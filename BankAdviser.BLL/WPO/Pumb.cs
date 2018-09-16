using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using System;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class Pumb : BankPage
    {
        public Pumb(IWebDriver driver, IBankManager bm, IDepositManager dm, DepositStatusHandler depositCollected)
            : base(driver, bm, dm, depositCollected)
        {            
            pageUrl = "https://retail.pumb.ua/ru/deposit";

            bankName = "ПУМБ";
            bankId = GetBankId(bankName);
        }

        private readonly string profitableUrl = "https://retail.pumb.ua/ru/deposit/profitable#calculator";

        private BankPage activePage;

        private IWebElement rate1MoWebEl;
        private IWebElement rate3MoWebEl;
        private IWebElement rate6MoWebEl;
        private IWebElement rate9MoWebEl;
        private IWebElement rate12MoWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            GoToUrl(pageUrl);

            // Доходный            
            GetDepositDokhodniy();            

            Successor?.CollectData();

            IsDriverRunning = false;
        }
        
        private void GetDepositDokhodniy()
        {
            GoToUrl(profitableUrl);

            Wait();

            // From 2500 UAH, % - on completion
            DepositDTO dokhodniyFrom2500UahOnCompl = new DepositDTO();
            dokhodniyFrom2500UahOnCompl.BankId = bankId;
            dokhodniyFrom2500UahOnCompl.Name = "Доходный";
            dokhodniyFrom2500UahOnCompl.Currency = Currency.UAH;
            dokhodniyFrom2500UahOnCompl.MinSum = 2500;
            dokhodniyFrom2500UahOnCompl.MaxSum = 200000;
            dokhodniyFrom2500UahOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom2500UahOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom2500UahOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom2500UahOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom2500UahOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[6]"));
            dokhodniyFrom2500UahOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom2500UahOnCompl.IsAddable = false;
            dokhodniyFrom2500UahOnCompl.IsWithdrawable = false;
            dokhodniyFrom2500UahOnCompl.IsCancellable = false;
            dokhodniyFrom2500UahOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom2500UahOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom2500UahOnCompl);

            // From 200k UAH, % - on completion
            DepositDTO dokhodniyFrom200kUahOnCompl = new DepositDTO();
            dokhodniyFrom200kUahOnCompl.BankId = bankId;
            dokhodniyFrom200kUahOnCompl.Name = "Доходный";
            dokhodniyFrom200kUahOnCompl.Currency = Currency.UAH;
            dokhodniyFrom200kUahOnCompl.MinSum = 200_000;
            dokhodniyFrom200kUahOnCompl.MaxSum = double.MaxValue;
            dokhodniyFrom200kUahOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom200kUahOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom200kUahOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom200kUahOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom200kUahOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[6]"));
            dokhodniyFrom200kUahOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom200kUahOnCompl.IsAddable = false;
            dokhodniyFrom200kUahOnCompl.IsWithdrawable = false;
            dokhodniyFrom200kUahOnCompl.IsCancellable = false;
            dokhodniyFrom200kUahOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom200kUahOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom200kUahOnCompl);

            // Go to monthly UAH
            IWebElement goToMonthlyUah = FindElement(By.XPath("(//li[@data-id='1'])[4]"));
            goToMonthlyUah.Click();

            Wait();

            // From 2500 UAH, % - monthly
            DepositDTO dokhodniyFrom2500UahMonthly = new DepositDTO();
            dokhodniyFrom2500UahMonthly.BankId = bankId;
            dokhodniyFrom2500UahMonthly.Name = "Доходный";
            dokhodniyFrom2500UahMonthly.Currency = Currency.UAH;
            dokhodniyFrom2500UahMonthly.MinSum = 2500;
            dokhodniyFrom2500UahMonthly.MaxSum = 200000;
            dokhodniyFrom2500UahMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom2500UahMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom2500UahMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom2500UahMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom2500UahMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom2500UahMonthly.IsAddable = false;
            dokhodniyFrom2500UahMonthly.IsWithdrawable = false;
            dokhodniyFrom2500UahMonthly.IsCancellable = false;
            dokhodniyFrom2500UahMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom2500UahMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom2500UahMonthly);

            // From 200k UAH, % - monthly
            DepositDTO dokhodniyFrom200kUahMonthly = new DepositDTO();
            dokhodniyFrom200kUahMonthly.BankId = bankId;
            dokhodniyFrom200kUahMonthly.Name = "Доходный";
            dokhodniyFrom200kUahMonthly.Currency = Currency.UAH;
            dokhodniyFrom200kUahMonthly.MinSum = 200_000;
            dokhodniyFrom200kUahMonthly.MaxSum = double.MaxValue;
            dokhodniyFrom200kUahMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom200kUahMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom200kUahMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom200kUahMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom200kUahMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom200kUahMonthly.IsAddable = false;
            dokhodniyFrom200kUahMonthly.IsWithdrawable = false;
            dokhodniyFrom200kUahMonthly.IsCancellable = false;
            dokhodniyFrom200kUahMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom200kUahMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom200kUahMonthly);

            // Go to USD
            IWebElement goToUsd = FindElement(By.XPath("//span[contains(.,'USD')]"));
            goToUsd.Click();

            Wait();

            // From 100 USD, % - on completion
            DepositDTO dokhodniyFrom100UsdOnCompl = new DepositDTO();
            dokhodniyFrom100UsdOnCompl.BankId = bankId;
            dokhodniyFrom100UsdOnCompl.Name = "Доходный";
            dokhodniyFrom100UsdOnCompl.Currency = Currency.USD;
            dokhodniyFrom100UsdOnCompl.MinSum = 100;
            dokhodniyFrom100UsdOnCompl.MaxSum = 20_000;
            dokhodniyFrom100UsdOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom100UsdOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom100UsdOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom100UsdOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom100UsdOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[6]"));
            dokhodniyFrom100UsdOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom100UsdOnCompl.IsAddable = false;
            dokhodniyFrom100UsdOnCompl.IsWithdrawable = false;
            dokhodniyFrom100UsdOnCompl.IsCancellable = false;
            dokhodniyFrom100UsdOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom100UsdOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom100UsdOnCompl);

            // From 20k USD, % - on completion
            DepositDTO dokhodniyFrom20kUsdOnCompl = new DepositDTO();
            dokhodniyFrom20kUsdOnCompl.BankId = bankId;
            dokhodniyFrom20kUsdOnCompl.Name = "Доходный";
            dokhodniyFrom20kUsdOnCompl.Currency = Currency.USD;
            dokhodniyFrom20kUsdOnCompl.MinSum = 20_000;
            dokhodniyFrom20kUsdOnCompl.MaxSum = double.MaxValue;
            dokhodniyFrom20kUsdOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom20kUsdOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom20kUsdOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom20kUsdOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom20kUsdOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[6]"));
            dokhodniyFrom20kUsdOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom20kUsdOnCompl.IsAddable = false;
            dokhodniyFrom20kUsdOnCompl.IsWithdrawable = false;
            dokhodniyFrom20kUsdOnCompl.IsCancellable = false;
            dokhodniyFrom20kUsdOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom20kUsdOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom20kUsdOnCompl);

            // Go to monthly USD
            IWebElement goToMonthlyUsd = FindElement(By.XPath("(//li[@data-id='1'])[7]"));
            goToMonthlyUsd.Click();

            Wait();

            // From 100 USD, % - monthly
            DepositDTO dokhodniyFrom100UsdMonthly = new DepositDTO();
            dokhodniyFrom100UsdMonthly.BankId = bankId;
            dokhodniyFrom100UsdMonthly.Name = "Доходный";
            dokhodniyFrom100UsdMonthly.Currency = Currency.USD;
            dokhodniyFrom100UsdMonthly.MinSum = 100;
            dokhodniyFrom100UsdMonthly.MaxSum = 20_000;
            dokhodniyFrom100UsdMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom100UsdMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom100UsdMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom100UsdMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom100UsdMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom100UsdMonthly.IsAddable = false;
            dokhodniyFrom100UsdMonthly.IsWithdrawable = false;
            dokhodniyFrom100UsdMonthly.IsCancellable = false;
            dokhodniyFrom100UsdMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom100UsdMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom100UsdMonthly);

            // From 20k USD, % - monthly
            DepositDTO dokhodniyFrom20kUsdMonthly = new DepositDTO();
            dokhodniyFrom20kUsdMonthly.BankId = bankId;
            dokhodniyFrom20kUsdMonthly.Name = "Доходный";
            dokhodniyFrom20kUsdMonthly.Currency = Currency.USD;
            dokhodniyFrom20kUsdMonthly.MinSum = 20_000;
            dokhodniyFrom20kUsdMonthly.MaxSum = double.MaxValue;
            dokhodniyFrom20kUsdMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom20kUsdMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom20kUsdMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom20kUsdMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom20kUsdMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom20kUsdMonthly.IsAddable = false;
            dokhodniyFrom20kUsdMonthly.IsWithdrawable = false;
            dokhodniyFrom20kUsdMonthly.IsCancellable = false;
            dokhodniyFrom20kUsdMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom20kUsdMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom20kUsdMonthly);

            // Go to EUR
            IWebElement goToEur = FindElement(By.XPath("//span[contains(.,'EUR')]"));
            goToEur.Click();

            Wait();

            // From 100 EUR, % - on completion
            DepositDTO dokhodniyFrom100EurOnCompl = new DepositDTO();
            dokhodniyFrom100EurOnCompl.BankId = bankId;
            dokhodniyFrom100EurOnCompl.Name = "Доходный";
            dokhodniyFrom100EurOnCompl.Currency = Currency.EUR;
            dokhodniyFrom100EurOnCompl.MinSum = 100;
            dokhodniyFrom100EurOnCompl.MaxSum = 20_000;
            dokhodniyFrom100EurOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom100EurOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom100EurOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom100EurOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom100EurOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[6]"));
            dokhodniyFrom100EurOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom100EurOnCompl.IsAddable = false;
            dokhodniyFrom100EurOnCompl.IsWithdrawable = false;
            dokhodniyFrom100EurOnCompl.IsCancellable = false;
            dokhodniyFrom100EurOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom100EurOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom100EurOnCompl);

            // From 20k EUR, % - on completion
            DepositDTO dokhodniyFrom20kEurOnCompl = new DepositDTO();
            dokhodniyFrom20kEurOnCompl.BankId = bankId;
            dokhodniyFrom20kEurOnCompl.Name = "Доходный";
            dokhodniyFrom20kEurOnCompl.Currency = Currency.EUR;
            dokhodniyFrom20kEurOnCompl.MinSum = 200_000;
            dokhodniyFrom20kEurOnCompl.MaxSum = double.MaxValue;
            dokhodniyFrom20kEurOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom20kEurOnCompl.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom20kEurOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom20kEurOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom20kEurOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[6]"));
            dokhodniyFrom20kEurOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom20kEurOnCompl.IsAddable = false;
            dokhodniyFrom20kEurOnCompl.IsWithdrawable = false;
            dokhodniyFrom20kEurOnCompl.IsCancellable = false;
            dokhodniyFrom20kEurOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom20kEurOnCompl);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom20kEurOnCompl);

            // Go to monthly EUR
            IWebElement goToMonthlyEur = FindElement(By.XPath("(//li[@data-id='1'])[9]"));
            goToMonthlyEur.Click();

            Wait();

            // From 100 EUR, % - monthly
            DepositDTO dokhodniyFrom100EurMonthly = new DepositDTO();
            dokhodniyFrom100EurMonthly.BankId = bankId;
            dokhodniyFrom100EurMonthly.Name = "Доходный";
            dokhodniyFrom100EurMonthly.Currency = Currency.EUR;
            dokhodniyFrom100EurMonthly.MinSum = 100;
            dokhodniyFrom100EurMonthly.MaxSum = 20_000;
            dokhodniyFrom100EurMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            dokhodniyFrom100EurMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            dokhodniyFrom100EurMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            dokhodniyFrom100EurMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            dokhodniyFrom100EurMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom100EurMonthly.IsAddable = false;
            dokhodniyFrom100EurMonthly.IsWithdrawable = false;
            dokhodniyFrom100EurMonthly.IsCancellable = false;
            dokhodniyFrom100EurMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom100EurMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom100EurMonthly);

            // From 20k EUR, % - monthly
            DepositDTO dokhodniyFrom20kEurMonthly = new DepositDTO();
            dokhodniyFrom20kEurMonthly.BankId = bankId;
            dokhodniyFrom20kEurMonthly.Name = "Доходный";
            dokhodniyFrom20kEurMonthly.Currency = Currency.EUR;
            dokhodniyFrom20kEurMonthly.MinSum = 20_000;
            dokhodniyFrom20kEurMonthly.MaxSum = double.MaxValue;
            dokhodniyFrom20kEurMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            dokhodniyFrom20kEurMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            dokhodniyFrom20kEurMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            dokhodniyFrom20kEurMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            dokhodniyFrom20kEurMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            dokhodniyFrom20kEurMonthly.IsAddable = false;
            dokhodniyFrom20kEurMonthly.IsWithdrawable = false;
            dokhodniyFrom20kEurMonthly.IsCancellable = false;
            dokhodniyFrom20kEurMonthly.Url = driver.Url;

            depositManager.SaveDeposit(dokhodniyFrom20kEurMonthly);
            WriteLogOnDepositCollected(bankName, dokhodniyFrom20kEurMonthly);
        }
        
    }
}