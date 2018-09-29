using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class Oshad : BankPage
    {
        public Oshad(IWebDriver driver, IBankManager bm, IDepositManager dm, DepositStatusHandler depositCollected)
            : base(driver, bm, dm, depositCollected)
        {
            bankName = "Ощадбанк";
            bankId = GetBankId(bankName);
        }

        private const string depositMoyUrl = "https://www.oschadbank.ua/ru/private/deposit/my-deposit/";
        private const string depositPensionniyUrl = "https://www.oschadbank.ua/ru/private/deposit/pensyniy-deposit/";
        private const string depositProgressivniyUrl = "https://www.oschadbank.ua/ru/private/deposit/progressive-deposit/";

        private IWebElement rate1MoWebEl;
        private IWebElement rate3MoWebEl;
        private IWebElement rate6MoWebEl;
        private IWebElement rate12MoWebEl;
        private IWebElement rate18MoWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            CollectDeposits();

            Successor?.CollectData();

            IsDriverRunning = false;
        }

        private void CollectDeposits()
        {
            // Moy:
            GoToUrl(depositMoyUrl);
            GetDepositMoy_Uah();
            GoToUsd();
            GetDepositMoy_Usd();
            GoToEur();
            GetDepositMoy_Eur();

            // Pensionniy:
            GoToUrl(depositPensionniyUrl);
            GetDepositPensionniy_Uah();
            GoToUsd();
            GetDepositPensionniy_Usd();
            GoToEur();
            GetDepositPensionniy_Eur();

            // Progressivniy:
            GoToUrl(depositProgressivniyUrl);
            GetDepositProgressivniy_Uah();
            GoToUsd();
            GetDepositProgressivniy_Usd();
            GoToEur();
            GetDepositProgressivniy_Eur();
        }

        private void GoToUsd()
        {
            IWebElement dropDown = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/nav[1]/div[1]/a[1]"));
            dropDown.Click();
            IWebElement goToUsd = WaitUntilElementClickable(By.XPath("(//a[contains(.,'$ в долларах США')])[1]"));
            goToUsd.Click();
        }       
        private void GoToEur()
        {
            IWebElement dropDown = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/nav[1]/div[1]/a[1]"));
            dropDown.Click();
            IWebElement goToEur = WaitUntilElementClickable(By.XPath("(//a[contains(.,'€ в Евро')])[1]"));
            goToEur.Click();
        }        
                
        private void GetDepositMoy_Uah()
        {            
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит";
            deposit.Currency = Currency.UAH;
            deposit.MinSum = 1000;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositMoy_Usd()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит";
            deposit.Currency = Currency.USD;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositMoy_Eur()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит";
            deposit.Currency = Currency.EUR;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }

        private void GetDepositPensionniy_Uah()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой пенсионный депозит";
            deposit.Currency = Currency.UAH;
            deposit.MinSum = 1000;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[1]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositPensionniy_Usd()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой пенсионный депозит";
            deposit.Currency = Currency.USD;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[1]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositPensionniy_Eur()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой пенсионный депозит";
            deposit.Currency = Currency.EUR;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[2]/td[1]"));
            deposit.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[3]/td[1]"));
            deposit.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[4]/td[1]"));
            deposit.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[1]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }

        private void GetDepositProgressivniy_Uah()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит прогрессивный";
            deposit.Currency = Currency.UAH;
            deposit.MinSum = 1000;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);

            deposit.IsAddable = false;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.BonusInfo = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[6]")).Text;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[7]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositProgressivniy_Usd()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит прогрессивный";
            deposit.Currency = Currency.USD;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);

            deposit.IsAddable = false;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.BonusInfo = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[6]")).Text;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[7]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetDepositProgressivniy_Eur()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Мой депозит прогрессивный";
            deposit.Currency = Currency.EUR;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/table[1]/tbody[1]/tr[1]/td[1]"));
            deposit.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);

            deposit.IsAddable = false;
            deposit.IsWithdrawable = false;
            deposit.IsCancellable = false;
            deposit.Url = driver.Url;

            deposit.BonusInfo = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[6]")).Text;

            deposit.Remark = FindElement(By.XPath("/html[1]/body[1]/div[3]/div[4]/main[1]/div[1]/header[1]/div[1]/div[1]/div[2]/ul[1]/li[7]")).Text;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
    }
}