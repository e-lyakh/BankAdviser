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
            bankName = "Райффайзен Банк Аваль";
            bankId = GetBankId(bankName);
        }

        private const string depositClassicheskiyUrl = "https://www.aval.ua/ru/personal/accounts/vkladclassic/";
        private const string depositSberegatelniyUrl = "https://www.aval.ua/ru/personal/accounts/vkladoschadnij/";
        private const string depositBonusUrl = "https://www.aval.ua/ru/personal/accounts/vkladbonus/";
        private const string depositUniversalniyUrl = "https://www.aval.ua/ru/personal/accounts/vkladunviresum/";

        private IWebElement rate1MoWebEl;
        private IWebElement rate2MoWebEl;
        private IWebElement rate3MoWebEl;
        private IWebElement rate4MoWebEl;
        private IWebElement rate5MoWebEl;
        private IWebElement rate6MoWebEl;
        private IWebElement rate12MoWebEl;
        private IWebElement rateTermlessWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            CollectDeposits();

            Successor?.CollectData();

            IsDriverRunning = false;
        }

        private void CollectDeposits()
        {
            // Classicheskiy:
            GoToUrl(depositClassicheskiyUrl);            
            GetDepositClassic_Uah_To100k();
            GetDepositClassic_Uah_From100k();
            GetDepositClassic_Usd_To10k();
            GetDepositClassic_Usd_From10k();

            // Sberegatelniy:
            GoToUrl(depositSberegatelniyUrl);            
            GetDepositSberegatelniy_Uah_To100k();
            GetDepositSberegatelniy_Uah_From100k();
            GetDepositSberegatelniy_Usd_To10k();
            GetDepositSberegatelniy_Usd_From10k();

            // Bonus:
            GoToUrl(depositBonusUrl);            
            GetDepositBonus();

            // Universalniy:
            GoToUrl(depositUniversalniyUrl);            
            GetDepositUniversalniyUah();
        }

        private void GetDepositClassic_Uah_To100k()
        {
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
            rate2MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[3]"));
            classicTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[4]"));
            classicTo100kUah.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[5]"));
            classicTo100kUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[3]/td[6]"));
            classicTo100kUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicTo100kUah.IsAddable = false;
            classicTo100kUah.IsWithdrawable = false;
            classicTo100kUah.IsCancellable = true;
            classicTo100kUah.Url = driver.Url;

            depositManager.SaveDeposit(classicTo100kUah);

            WriteLogOnDepositCollected(bankName, classicTo100kUah);
        }
        private void GetDepositClassic_Uah_From100k()
        {
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
            rate2MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[3]"));
            classicFrom100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[4]"));
            classicFrom100kUah.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[5]"));
            classicFrom100kUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[4]/td[6]"));
            classicFrom100kUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicFrom100kUah.IsAddable = false;
            classicFrom100kUah.IsWithdrawable = false;
            classicFrom100kUah.IsCancellable = true;
            classicFrom100kUah.Url = driver.Url;

            depositManager.SaveDeposit(classicFrom100kUah);

            WriteLogOnDepositCollected(bankName, classicFrom100kUah);
        }
        private void GetDepositClassic_Usd_To10k()
        {
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
            rate2MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[3]"));
            classicTo10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[4]"));
            classicTo10kUsd.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[5]"));
            classicTo10kUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[7]/td[6]"));
            classicTo10kUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicTo10kUsd.IsAddable = false;
            classicTo10kUsd.IsWithdrawable = false;
            classicTo10kUsd.IsCancellable = true;
            classicTo10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(classicTo10kUsd);

            WriteLogOnDepositCollected(bankName, classicTo10kUsd);
        }
        private void GetDepositClassic_Usd_From10k()
        {
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
            rate2MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[3]"));
            classicFrom10kUsd.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[4]"));
            classicFrom10kUsd.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[5]"));
            classicFrom10kUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[8]/td[6]"));
            classicFrom10kUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            classicFrom10kUsd.IsAddable = false;
            classicFrom10kUsd.IsWithdrawable = false;
            classicFrom10kUsd.IsCancellable = true;
            classicFrom10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(classicFrom10kUsd);

            WriteLogOnDepositCollected(bankName, classicFrom10kUsd);
        }

        private void GetDepositSberegatelniy_Uah_To100k()
        {
            // To 100k UAH
            DepositDTO sberegatelniyTo100kUah = new DepositDTO();
            sberegatelniyTo100kUah.BankId = bankId;
            sberegatelniyTo100kUah.Name = "Сберегательный";
            sberegatelniyTo100kUah.Currency = Currency.UAH;
            sberegatelniyTo100kUah.MinSum = 500;
            sberegatelniyTo100kUah.MaxSum = 100_000;
            sberegatelniyTo100kUah.InterestsPeriodicity = InterestsPeriodicity.Monthly;            
            
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[3]/td[2]"));
            sberegatelniyTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[3]/td[3]"));
            sberegatelniyTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[3]/td[4]"));
            sberegatelniyTo100kUah.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            sberegatelniyTo100kUah.IsAddable = true;
            sberegatelniyTo100kUah.IsWithdrawable = false;
            sberegatelniyTo100kUah.IsCancellable = true;
            sberegatelniyTo100kUah.Url = driver.Url;

            depositManager.SaveDeposit(sberegatelniyTo100kUah);

            WriteLogOnDepositCollected(bankName, sberegatelniyTo100kUah);
        }
        private void GetDepositSberegatelniy_Uah_From100k()
        {
            // From 100k UAH
            DepositDTO sberegatelniyFrom100kUah = new DepositDTO();
            sberegatelniyFrom100kUah.BankId = bankId;
            sberegatelniyFrom100kUah.Name = "Сберегательный";
            sberegatelniyFrom100kUah.Currency = Currency.UAH;
            sberegatelniyFrom100kUah.MinSum = 100_000;
            sberegatelniyFrom100kUah.MaxSum = double.MaxValue;
            sberegatelniyFrom100kUah.InterestsPeriodicity = InterestsPeriodicity.Monthly;
            
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[4]/td[2]"));
            sberegatelniyFrom100kUah.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[4]/td[3]"));
            sberegatelniyFrom100kUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[4]/td[4]"));
            sberegatelniyFrom100kUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            sberegatelniyFrom100kUah.IsAddable = true;
            sberegatelniyFrom100kUah.IsWithdrawable = false;
            sberegatelniyFrom100kUah.IsCancellable = true;
            sberegatelniyFrom100kUah.Url = driver.Url;

            depositManager.SaveDeposit(sberegatelniyFrom100kUah);

            WriteLogOnDepositCollected(bankName, sberegatelniyFrom100kUah);
        }
        private void GetDepositSberegatelniy_Usd_To10k()
        {
            // To 10k USD
            DepositDTO sberegatelniyTo10kUsd = new DepositDTO();
            sberegatelniyTo10kUsd.BankId = bankId;
            sberegatelniyTo10kUsd.Name = "Сберегательный";
            sberegatelniyTo10kUsd.Currency = Currency.USD;
            sberegatelniyTo10kUsd.MinSum = 100;
            sberegatelniyTo10kUsd.MaxSum = 10_000;
            sberegatelniyTo10kUsd.InterestsPeriodicity = InterestsPeriodicity.Monthly;
            
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[7]/td[2]"));
            sberegatelniyTo10kUsd.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[7]/td[3]"));
            sberegatelniyTo10kUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[7]/td[4]"));
            sberegatelniyTo10kUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            sberegatelniyTo10kUsd.IsAddable = true;
            sberegatelniyTo10kUsd.IsWithdrawable = false;
            sberegatelniyTo10kUsd.IsCancellable = true;
            sberegatelniyTo10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(sberegatelniyTo10kUsd);

            WriteLogOnDepositCollected(bankName, sberegatelniyTo10kUsd);
        }
        private void GetDepositSberegatelniy_Usd_From10k()
        {
            // From 10k USD
            DepositDTO sberegatelniyFrom10kUsd = new DepositDTO();
            sberegatelniyFrom10kUsd.BankId = bankId;
            sberegatelniyFrom10kUsd.Name = "Сберегательный";
            sberegatelniyFrom10kUsd.Currency = Currency.USD;
            sberegatelniyFrom10kUsd.MinSum = 10_000;
            sberegatelniyFrom10kUsd.MaxSum = double.MaxValue;
            sberegatelniyFrom10kUsd.InterestsPeriodicity = InterestsPeriodicity.Monthly;
            
            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[8]/td[2]"));
            sberegatelniyFrom10kUsd.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[8]/td[3]"));
            sberegatelniyFrom10kUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[2]/tbody[1]/tr[8]/td[4]"));
            sberegatelniyFrom10kUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            sberegatelniyFrom10kUsd.IsAddable = true;
            sberegatelniyFrom10kUsd.IsWithdrawable = false;
            sberegatelniyFrom10kUsd.IsCancellable = true;
            sberegatelniyFrom10kUsd.Url = driver.Url;

            depositManager.SaveDeposit(sberegatelniyFrom10kUsd);

            WriteLogOnDepositCollected(bankName, sberegatelniyFrom10kUsd);
        }

        private void GetDepositBonus()
        {
            DepositDTO bonus = new DepositDTO();
            bonus.BankId = bankId;
            bonus.Name = "Bonus";
            bonus.Currency = Currency.UAH;
            bonus.MinSum = 2000;
            bonus.MaxSum = double.MaxValue;
            bonus.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate1MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[2]"));
            bonus.Rate1Months = MyConvertTo.StrWithDotAndPercentToDouble(rate1MoWebEl.Text);
            rate2MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[3]"));
            bonus.Rate2Months = MyConvertTo.StrWithDotAndPercentToDouble(rate2MoWebEl.Text);
            rate3MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[4]"));
            bonus.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate4MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[5]"));
            bonus.Rate4Months = MyConvertTo.StrWithDotAndPercentToDouble(rate4MoWebEl.Text);
            rate5MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[6]"));
            bonus.Rate5Months = MyConvertTo.StrWithDotAndPercentToDouble(rate5MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/table[1]/tbody[1]/tr[2]/td[7]"));
            bonus.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);

            bonus.IsAddable = false;
            bonus.IsWithdrawable = true;
            bonus.IsCancellable = true;
            bonus.Url = driver.Url;

            depositManager.SaveDeposit(bonus);

            WriteLogOnDepositCollected(bankName, bonus);
        }

        private void GetDepositUniversalniyUah()
        {
            DepositDTO universalniy = new DepositDTO();
            universalniy.BankId = bankId;
            universalniy.Name = "Универсальный";
            universalniy.Currency = Currency.UAH;
            universalniy.MinSum = 100;
            universalniy.MaxSum = double.MaxValue;
            universalniy.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rateTermlessWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/p[1]/strong[1]"));
            universalniy.RateTermless = MyConvertTo.StrWithDotAndPercentToDouble(rateTermlessWebEl.Text);

            universalniy.IsAddable = true;
            universalniy.IsWithdrawable = true;
            universalniy.IsCancellable = true;
            universalniy.Url = driver.Url;

            universalniy.Remark = FindElement(By.XPath("/html[1]/body[1]/div[1]/table[1]/tbody[1]/tr[2]/td[2]/p[1]")).Text;

            depositManager.SaveDeposit(universalniy);

            WriteLogOnDepositCollected(bankName, universalniy);
        }
    }
}