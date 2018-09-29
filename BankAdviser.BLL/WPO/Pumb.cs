using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class Pumb : BankPage
    {
        public Pumb(IWebDriver driver, IBankManager bm, IDepositManager dm, DepositStatusHandler depositCollected)
            : base(driver, bm, dm, depositCollected)
        {
            bankName = "ПУМБ";
            bankId = GetBankId(bankName);
        }

        private const string depositDoknodiyUrl = "https://retail.pumb.ua/ru/deposit/profitable#calculator";
        private const string depositSpokoyniyUrl = "https://retail.pumb.ua/ru/deposit/calm#calculator";

        private IWebElement rate1MoWebEl;
        private IWebElement rate3MoWebEl;
        private IWebElement rate6MoWebEl;
        private IWebElement rate9MoWebEl;
        private IWebElement rate12MoWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            CollectDeposits();

            Successor?.CollectData();

            IsDriverRunning = false;
        }

        private void CollectDeposits()
        {
            // Dokhodniy:
            GoToUrl(depositDoknodiyUrl);            

            GetDepositDokhodniy_Uah_OnCompletion();

            GoToUahMonthly();
            GetDepositDokhodniy_Uah_Monthly();

            GoToUsd();
            GetDepositDokhodniy_Usd_OnCompletion();

            GoToUsdMonthly();
            GetDepositDokhodniy_Usd_Monthly();

            GoToEur();
            GetDepositDokhodniy_Eur_OnCompletion();

            GoToEurMonthly();
            GetDepositDokhodniy_Eur_Monthly();

            // Spokoyniy:
            GoToUrl(depositSpokoyniyUrl);            

            GetDepositSpokoyniy_Uah_OnCompletion();

            GoToUahMonthly();
            GetDepositSpokoyniy_Uah_Monthly();

            GoToUsd();
            GetDepositSpokoyniy_Usd_OnCompletion();

            GoToUsdMonthly();
            GetDepositSpokoyniy_Usd_Monthly();

            GoToEur();
            GetDepositSpokoyniy_Eur_OnCompletion();

            GoToEurMonthly();
            GetDepositSpokoyniy_Eur_Monthly();
        }

        private void GoToUah()
        {
            IWebElement goToUah = FindElement(By.XPath("//span[contains(.,'UAH')]"));
            goToUah.Click();            
        }
        private void GoToUahMonthly()
        {
            IWebElement goToMonthlyUah = FindElement(By.XPath("(//li[@data-id='1'])[4]"));
            goToMonthlyUah.Click();            
        }
        private void GoToUsd()
        {
            IWebElement goToUsd = FindElement(By.XPath("//span[contains(.,'USD')]"));
            goToUsd.Click();            
        }
        private void GoToUsdMonthly()
        {
            IWebElement goToMonthlyUsd = FindElement(By.XPath("(//li[@data-id='1'])[7]"));
            goToMonthlyUsd.Click();            
        }
        private void GoToEur()
        {
            IWebElement goToEur = FindElement(By.XPath("//span[contains(.,'EUR')]"));
            goToEur.Click();            
        }
        private void GoToEurMonthly()
        {
            IWebElement goToMonthlyEur = FindElement(By.XPath("(//li[@data-id='1'])[9]"));
            goToMonthlyEur.Click();            
        }
                
        private void GetDepositDokhodniy_Uah_OnCompletion()
        {
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
        }        
        private void GetDepositDokhodniy_Uah_Monthly()
        {
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
        }
        private void GetDepositDokhodniy_Usd_OnCompletion()
        {
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
        }
        private void GetDepositDokhodniy_Usd_Monthly()
        {
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
        }
        private void GetDepositDokhodniy_Eur_OnCompletion()
        {
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
        }
        private void GetDepositDokhodniy_Eur_Monthly()
        {
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

        private void GetDepositSpokoyniy_Uah_OnCompletion()
        {
            // From 2500 UAH, % - on completion
            DepositDTO spokoyniyFrom2500UahOnCompl = new DepositDTO();
            spokoyniyFrom2500UahOnCompl.BankId = bankId;
            spokoyniyFrom2500UahOnCompl.Name = "Спокойный";
            spokoyniyFrom2500UahOnCompl.Currency = Currency.UAH;
            spokoyniyFrom2500UahOnCompl.MinSum = 2500;
            spokoyniyFrom2500UahOnCompl.MaxSum = 200000;
            spokoyniyFrom2500UahOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom2500UahOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom2500UahOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom2500UahOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom2500UahOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom2500UahOnCompl.IsAddable = false;
            spokoyniyFrom2500UahOnCompl.IsWithdrawable = false;
            spokoyniyFrom2500UahOnCompl.IsCancellable = true;
            spokoyniyFrom2500UahOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom2500UahOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom2500UahOnCompl);

            // From 200k UAH, % - on completion
            DepositDTO spokoyniyFrom200kUahOnCompl = new DepositDTO();
            spokoyniyFrom200kUahOnCompl.BankId = bankId;
            spokoyniyFrom200kUahOnCompl.Name = "Спокойный";
            spokoyniyFrom200kUahOnCompl.Currency = Currency.UAH;
            spokoyniyFrom200kUahOnCompl.MinSum = 200_000;
            spokoyniyFrom200kUahOnCompl.MaxSum = double.MaxValue;
            spokoyniyFrom200kUahOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom200kUahOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom200kUahOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom200kUahOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom200kUahOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom200kUahOnCompl.IsAddable = false;
            spokoyniyFrom200kUahOnCompl.IsWithdrawable = false;
            spokoyniyFrom200kUahOnCompl.IsCancellable = true;
            spokoyniyFrom200kUahOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom200kUahOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom200kUahOnCompl);
        }
        private void GetDepositSpokoyniy_Uah_Monthly()
        {
            // From 2500 UAH, % - monthly
            DepositDTO spokoyniyFrom2500UahMonthly = new DepositDTO();
            spokoyniyFrom2500UahMonthly.BankId = bankId;
            spokoyniyFrom2500UahMonthly.Name = "Спокойный";
            spokoyniyFrom2500UahMonthly.Currency = Currency.UAH;
            spokoyniyFrom2500UahMonthly.MinSum = 2500;
            spokoyniyFrom2500UahMonthly.MaxSum = 200000;
            spokoyniyFrom2500UahMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom2500UahMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom2500UahMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom2500UahMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom2500UahMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom2500UahMonthly.IsAddable = false;
            spokoyniyFrom2500UahMonthly.IsWithdrawable = false;
            spokoyniyFrom2500UahMonthly.IsCancellable = true;
            spokoyniyFrom2500UahMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom2500UahMonthly);
            WriteLogOnDepositCollected(bankName, spokoyniyFrom2500UahMonthly);

            // From 200k UAH, % - monthly
            DepositDTO spokoyniyFrom200kUahMonthly = new DepositDTO();
            spokoyniyFrom200kUahMonthly.BankId = bankId;
            spokoyniyFrom200kUahMonthly.Name = "Спокойный";
            spokoyniyFrom200kUahMonthly.Currency = Currency.UAH;
            spokoyniyFrom200kUahMonthly.MinSum = 200_000;
            spokoyniyFrom200kUahMonthly.MaxSum = double.MaxValue;
            spokoyniyFrom200kUahMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom200kUahMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom200kUahMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom200kUahMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[1]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom200kUahMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom200kUahMonthly.IsAddable = false;
            spokoyniyFrom200kUahMonthly.IsWithdrawable = false;
            spokoyniyFrom200kUahMonthly.IsCancellable = true;
            spokoyniyFrom200kUahMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom200kUahMonthly);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom200kUahMonthly);
        }
        private void GetDepositSpokoyniy_Usd_OnCompletion()
        {
            // From 100 USD, % - on completion
            DepositDTO spokoyniyFrom100UsdOnCompl = new DepositDTO();
            spokoyniyFrom100UsdOnCompl.BankId = bankId;
            spokoyniyFrom100UsdOnCompl.Name = "Спокойный";
            spokoyniyFrom100UsdOnCompl.Currency = Currency.USD;
            spokoyniyFrom100UsdOnCompl.MinSum = 100;
            spokoyniyFrom100UsdOnCompl.MaxSum = 20_000;
            spokoyniyFrom100UsdOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom100UsdOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom100UsdOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom100UsdOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom100UsdOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom100UsdOnCompl.IsAddable = false;
            spokoyniyFrom100UsdOnCompl.IsWithdrawable = false;
            spokoyniyFrom100UsdOnCompl.IsCancellable = true;
            spokoyniyFrom100UsdOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom100UsdOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom100UsdOnCompl);

            // From 20k USD, % - on completion
            DepositDTO spokoyniyFrom20kUsdOnCompl = new DepositDTO();
            spokoyniyFrom20kUsdOnCompl.BankId = bankId;
            spokoyniyFrom20kUsdOnCompl.Name = "Спокойный";
            spokoyniyFrom20kUsdOnCompl.Currency = Currency.USD;
            spokoyniyFrom20kUsdOnCompl.MinSum = 20_000;
            spokoyniyFrom20kUsdOnCompl.MaxSum = double.MaxValue;
            spokoyniyFrom20kUsdOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom20kUsdOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom20kUsdOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom20kUsdOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom20kUsdOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom20kUsdOnCompl.IsAddable = false;
            spokoyniyFrom20kUsdOnCompl.IsWithdrawable = false;
            spokoyniyFrom20kUsdOnCompl.IsCancellable = true;
            spokoyniyFrom20kUsdOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom20kUsdOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom20kUsdOnCompl);
        }
        private void GetDepositSpokoyniy_Usd_Monthly()
        {
            // From 100 USD, % - monthly
            DepositDTO spokoyniyFrom100UsdMonthly = new DepositDTO();
            spokoyniyFrom100UsdMonthly.BankId = bankId;
            spokoyniyFrom100UsdMonthly.Name = "Спокойный";
            spokoyniyFrom100UsdMonthly.Currency = Currency.USD;
            spokoyniyFrom100UsdMonthly.MinSum = 100;
            spokoyniyFrom100UsdMonthly.MaxSum = 20_000;
            spokoyniyFrom100UsdMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom100UsdMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom100UsdMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom100UsdMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom100UsdMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom100UsdMonthly.IsAddable = false;
            spokoyniyFrom100UsdMonthly.IsWithdrawable = false;
            spokoyniyFrom100UsdMonthly.IsCancellable = true;
            spokoyniyFrom100UsdMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom100UsdMonthly);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom100UsdMonthly);

            // From 20k USD, % - monthly
            DepositDTO spokoyniyFrom20kUsdMonthly = new DepositDTO();
            spokoyniyFrom20kUsdMonthly.BankId = bankId;
            spokoyniyFrom20kUsdMonthly.Name = "Спокойный";
            spokoyniyFrom20kUsdMonthly.Currency = Currency.USD;
            spokoyniyFrom20kUsdMonthly.MinSum = 20_000;
            spokoyniyFrom20kUsdMonthly.MaxSum = double.MaxValue;
            spokoyniyFrom20kUsdMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom20kUsdMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom20kUsdMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom20kUsdMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[2]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom20kUsdMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom20kUsdMonthly.IsAddable = false;
            spokoyniyFrom20kUsdMonthly.IsWithdrawable = false;
            spokoyniyFrom20kUsdMonthly.IsCancellable = true;
            spokoyniyFrom20kUsdMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom20kUsdMonthly);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom20kUsdMonthly);
        }
        private void GetDepositSpokoyniy_Eur_OnCompletion()
        {
            // From 100 EUR, % - on completion
            DepositDTO spokoyniyFrom100EurOnCompl = new DepositDTO();
            spokoyniyFrom100EurOnCompl.BankId = bankId;
            spokoyniyFrom100EurOnCompl.Name = "Спокойный";
            spokoyniyFrom100EurOnCompl.Currency = Currency.EUR;
            spokoyniyFrom100EurOnCompl.MinSum = 100;
            spokoyniyFrom100EurOnCompl.MaxSum = 20_000;
            spokoyniyFrom100EurOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom100EurOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom100EurOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom100EurOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom100EurOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom100EurOnCompl.IsAddable = false;
            spokoyniyFrom100EurOnCompl.IsWithdrawable = false;
            spokoyniyFrom100EurOnCompl.IsCancellable = true;
            spokoyniyFrom100EurOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom100EurOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom100EurOnCompl);

            // From 20k EUR, % - on completion
            DepositDTO spokoyniyFrom20kEurOnCompl = new DepositDTO();
            spokoyniyFrom20kEurOnCompl.BankId = bankId;
            spokoyniyFrom20kEurOnCompl.Name = "Спокойный";
            spokoyniyFrom20kEurOnCompl.Currency = Currency.EUR;
            spokoyniyFrom20kEurOnCompl.MinSum = 200_000;
            spokoyniyFrom20kEurOnCompl.MaxSum = double.MaxValue;
            spokoyniyFrom20kEurOnCompl.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom20kEurOnCompl.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom20kEurOnCompl.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom20kEurOnCompl.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[1]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom20kEurOnCompl.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom20kEurOnCompl.IsAddable = false;
            spokoyniyFrom20kEurOnCompl.IsWithdrawable = false;
            spokoyniyFrom20kEurOnCompl.IsCancellable = true;
            spokoyniyFrom20kEurOnCompl.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom20kEurOnCompl);

            WriteLogOnDepositCollected(bankName, spokoyniyFrom20kEurOnCompl);
        }
        private void GetDepositSpokoyniy_Eur_Monthly()
        {

            // From 100 EUR, % - monthly
            DepositDTO spokoyniyFrom100EurMonthly = new DepositDTO();
            spokoyniyFrom100EurMonthly.BankId = bankId;
            spokoyniyFrom100EurMonthly.Name = "Спокойный";
            spokoyniyFrom100EurMonthly.Currency = Currency.EUR;
            spokoyniyFrom100EurMonthly.MinSum = 100;
            spokoyniyFrom100EurMonthly.MaxSum = 20_000;
            spokoyniyFrom100EurMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            spokoyniyFrom100EurMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            spokoyniyFrom100EurMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            spokoyniyFrom100EurMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            spokoyniyFrom100EurMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom100EurMonthly.IsAddable = false;
            spokoyniyFrom100EurMonthly.IsWithdrawable = false;
            spokoyniyFrom100EurMonthly.IsCancellable = true;
            spokoyniyFrom100EurMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom100EurMonthly);
            WriteLogOnDepositCollected(bankName, spokoyniyFrom100EurMonthly);

            // From 20k EUR, % - monthly
            DepositDTO spokoyniyFrom20kEurMonthly = new DepositDTO();
            spokoyniyFrom20kEurMonthly.BankId = bankId;
            spokoyniyFrom20kEurMonthly.Name = "Спокойный";
            spokoyniyFrom20kEurMonthly.Currency = Currency.EUR;
            spokoyniyFrom20kEurMonthly.MinSum = 20_000;
            spokoyniyFrom20kEurMonthly.MaxSum = double.MaxValue;
            spokoyniyFrom20kEurMonthly.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            spokoyniyFrom20kEurMonthly.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            spokoyniyFrom20kEurMonthly.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate9MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            spokoyniyFrom20kEurMonthly.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[4]/div[1]/section[2]/div[2]/div[2]/div[3]/ul[1]/li[3]/div[1]/div[2]/ul[1]/li[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            spokoyniyFrom20kEurMonthly.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            spokoyniyFrom20kEurMonthly.IsAddable = false;
            spokoyniyFrom20kEurMonthly.IsWithdrawable = false;
            spokoyniyFrom20kEurMonthly.IsCancellable = true;
            spokoyniyFrom20kEurMonthly.Url = driver.Url;

            depositManager.SaveDeposit(spokoyniyFrom20kEurMonthly);
            WriteLogOnDepositCollected(bankName, spokoyniyFrom20kEurMonthly);
        }
    }
}