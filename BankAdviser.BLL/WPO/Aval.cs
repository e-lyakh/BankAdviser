using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class Aval : BankPage
    {
        public Aval(IWebDriver driver, IBankManager bankManager, IDepositManager depositManager, DepositStatusHandler depositCollected)
            : base(driver, bankManager, depositManager, depositCollected)
        {
            pageUrl = "https://www.aval.ua/ru/personal/accounts/";

            bankName = "Райффайзен Банк Аваль";
            bankId = GetBankId(bankName);
        }

        private readonly string classicUrl = "https://www.aval.ua/ru/personal/accounts/vkladclassic/";

        private IWebElement rate1MoWebEl;
        private IWebElement rate2MoWebEl;
        private IWebElement rate3MoWebEl;
        private IWebElement rate6MoWebEl;
        private IWebElement rate12MoWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            GoToUrl(pageUrl);

            // TODO collecting deposits data

            GetDepositClassic();

            Successor?.CollectData();

            IsDriverRunning = true;
        }

        private void GetDepositClassic()
        {
            GoToUrl(classicUrl);

            Wait();

            // To 100k UAH
            DepositDTO classicTo100kUah = new DepositDTO();
            classicTo100kUah.BankId = bankId;
            classicTo100kUah.Name = "Классический";
            classicTo100kUah.Currency = Currency.UAH;
            classicTo100kUah.MinSum = 2000;
            classicTo100kUah.MaxSum = 100_000;
            classicTo100kUah.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[2]"));
            classicTo100kUah.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate2MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            classicTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            classicTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            classicTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[6]"));
            classicTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicTo100kUah.IsAddable = false;
            classicTo100kUah.IsWithdrawable = false;
            classicTo100kUah.IsCancellable = true;
            classicTo100kUah.Url = driver.Url;

            depositManager.SaveDeposit(classicTo100kUah);
            WriteLogOnDepositCollected(bankName, classicTo100kUah);

            // From 100k UAH
            DepositDTO classicFrom100kUah = new DepositDTO();
            classicFrom100kUah.BankId = bankId;
            classicFrom100kUah.Name = "Классический";
            classicFrom100kUah.Currency = Currency.UAH;
            classicFrom100kUah.MinSum = 100_000;
            classicFrom100kUah.MaxSum = double.MaxValue;
            classicFrom100kUah.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[2]"));
            classicFrom100kUah.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate2MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[3]"));
            classicFrom100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[4]"));
            classicFrom100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[5]"));
            classicFrom100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[6]"));
            classicFrom100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicFrom100kUah.IsAddable = false;
            classicFrom100kUah.IsWithdrawable = false;
            classicFrom100kUah.IsCancellable = true;
            classicFrom100kUah.Url = driver.Url;

            depositManager.SaveDeposit(classicFrom100kUah);
            WriteLogOnDepositCollected(bankName, classicFrom100kUah);

            // To 10k USD
            DepositDTO classicTo10kUsd = new DepositDTO();
            classicTo10kUsd.BankId = bankId;
            classicTo10kUsd.Name = "Классический";
            classicTo10kUsd.Currency = Currency.USD;
            classicTo10kUsd.MinSum = 500;
            classicTo10kUsd.MaxSum = 10_000;
            classicTo10kUsd.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[2]"));
            classicTo10kUsd.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate2MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[3]"));
            classicTo10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[4]"));
            classicTo10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[5]"));
            classicTo10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[6]"));
            classicTo10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicTo10kUsd.IsAddable = false;
            classicTo10kUsd.IsWithdrawable = false;
            classicTo10kUsd.IsCancellable = true;
            classicTo10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(classicTo10kUsd);
            WriteLogOnDepositCollected(bankName, classicTo10kUsd);

            // From 10k USD
            DepositDTO classicFrom10kUsd = new DepositDTO();
            classicFrom10kUsd.BankId = bankId;
            classicFrom10kUsd.Name = "Классический";
            classicFrom10kUsd.Currency = Currency.USD;
            classicFrom10kUsd.MinSum = 10_000;
            classicFrom10kUsd.MaxSum = double.MaxValue;
            classicFrom10kUsd.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[2]"));
            classicFrom10kUsd.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate2MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[3]"));
            classicFrom10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[4]"));
            classicFrom10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[5]"));
            classicFrom10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[6]"));
            classicFrom10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicFrom10kUsd.IsAddable = false;
            classicFrom10kUsd.IsWithdrawable = false;
            classicFrom10kUsd.IsCancellable = true;
            classicFrom10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(classicFrom10kUsd);
            WriteLogOnDepositCollected(bankName, classicFrom10kUsd);
        }
    }
}