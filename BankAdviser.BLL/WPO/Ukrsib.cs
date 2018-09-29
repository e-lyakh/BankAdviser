using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class Ukrsib : BankPage
    {
        public Ukrsib(IWebDriver driver, IBankManager bm, IDepositManager dm, DepositStatusHandler depositCollected)
            : base(driver, bm, dm, depositCollected)
        {
            bankName = "Укрсиббанк";
            bankId = GetBankId(bankName);
        }

        private const string depositGarantirovaniyCapitalUrl = "https://my.ukrsibbank.com/ru/personal/deposits/at_the_end/";
        private const string depositNadezhniyDokhodUrl = "https://my.ukrsibbank.com/ru/personal/deposits/monthly/";
        private const string depositSchastliviyVozrastUrl = "https://my.ukrsibbank.com/ru/personal/deposits/replenishment/";
        private const string depositPersonalPlanUrl = "https://my.ukrsibbank.com/ru/personal/deposits/saving_plan/";
                
        private IWebElement rate9MoWebEl;
        private IWebElement rate12MoWebEl;
        private IWebElement rate18MoWebEl;
        private IWebElement rate24MoWebEl;

        public override void CollectData ()
        {
            IsDriverRunning = true;

            CollectDeposits();

            Successor?.CollectData();

            IsDriverRunning = false;
        }

        private void CollectDeposits()
        {
            // Garantirovaniy Capital:
            GoToUrl(depositGarantirovaniyCapitalUrl);
            GetGarantirovaniyCapital();

            // Nadezhniy Dokhod:
            GoToUrl(depositNadezhniyDokhodUrl);
            GetNadezhniyDokhod();

            // Schastliviy Vozrast:
            GoToUrl(depositSchastliviyVozrastUrl);
            GetSchastliviyVozrast();

            // Personalniy Plan Obogasheniya:
            GoToUrl(depositPersonalPlanUrl);
            GetPersonalPlan();
        }

        private void GetGarantirovaniyCapital()
        {            
            DepositDTO garantirCapital = new DepositDTO();
            garantirCapital.BankId = bankId;
            garantirCapital.Name = "Гарантированный капитал";
            garantirCapital.Currency = Currency.UAH;
            garantirCapital.MinSum = 1000;
            garantirCapital.MaxSum = double.MaxValue;
            garantirCapital.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            IWebElement conditions = FindElement(By.XPath("(//h3[contains(.,'УСЛОВИЯ')])[1]"));
            conditions.Click();

            rate9MoWebEl = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[2]"));
            garantirCapital.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[3]"));
            garantirCapital.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[4]"));
            garantirCapital.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);
            rate24MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[5]"));
            garantirCapital.Rate24Months = MyConvertTo.StrWithDotAndPercentToDouble(rate24MoWebEl.Text);

            garantirCapital.IsAddable = false;
            garantirCapital.IsWithdrawable = false;
            garantirCapital.IsCancellable = false;
            garantirCapital.Url = driver.Url;

            garantirCapital.BonusInfo = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/div[2]/ul[1]/li[4]")).Text;

            depositManager.SaveDeposit(garantirCapital);

            WriteLogOnDepositCollected(bankName, garantirCapital);
        }
        private void GetNadezhniyDokhod()
        {           
            DepositDTO nadezhniyDokhod = new DepositDTO();
            nadezhniyDokhod.BankId = bankId;
            nadezhniyDokhod.Name = "Надежный доход";
            nadezhniyDokhod.Currency = Currency.UAH;
            nadezhniyDokhod.MinSum = 1000;
            nadezhniyDokhod.MaxSum = double.MaxValue;
            nadezhniyDokhod.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            IWebElement conditions = FindElement(By.XPath("(//div[contains(.,'Условия')])[6]"));
            conditions.Click();

            rate9MoWebEl = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[2]"));
            nadezhniyDokhod.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[3]"));
            nadezhniyDokhod.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[4]"));
            nadezhniyDokhod.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);
            rate24MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[5]"));
            nadezhniyDokhod.Rate24Months = MyConvertTo.StrWithDotAndPercentToDouble(rate24MoWebEl.Text);

            nadezhniyDokhod.IsAddable = false;
            nadezhniyDokhod.IsWithdrawable = false;
            nadezhniyDokhod.IsCancellable = false;
            nadezhniyDokhod.Url = driver.Url;

            nadezhniyDokhod.BonusInfo = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/div[2]/ul[1]/li[3]")).Text;

            depositManager.SaveDeposit(nadezhniyDokhod);

            WriteLogOnDepositCollected(bankName, nadezhniyDokhod);
        }
        private void GetSchastliviyVozrast()
        {
            DepositDTO schastliviyVozrast = new DepositDTO();
            schastliviyVozrast.BankId = bankId;
            schastliviyVozrast.Name = "Счастливый возраст";
            schastliviyVozrast.Currency = Currency.UAH;
            schastliviyVozrast.MinSum = 100;
            schastliviyVozrast.MaxSum = double.MaxValue;
            schastliviyVozrast.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            IWebElement conditions = FindElement(By.XPath("(//div[contains(.,'УСЛОВИЯ')])[6]"));
            conditions.Click();

            rate9MoWebEl = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[2]"));
            schastliviyVozrast.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[3]"));
            schastliviyVozrast.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[4]"));
            schastliviyVozrast.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);
            rate24MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[2]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[5]"));
            schastliviyVozrast.Rate24Months = MyConvertTo.StrWithDotAndPercentToDouble(rate24MoWebEl.Text);

            schastliviyVozrast.IsAddable = true;
            schastliviyVozrast.IsWithdrawable = false;
            schastliviyVozrast.IsCancellable = false;
            schastliviyVozrast.Url = driver.Url;

            depositManager.SaveDeposit(schastliviyVozrast);

            WriteLogOnDepositCollected(bankName, schastliviyVozrast);
        }
        private void GetPersonalPlan()
        {
            DepositDTO personalPlan = new DepositDTO();
            personalPlan.BankId = bankId;
            personalPlan.Name = "Персональный план обогащения";
            personalPlan.Currency = Currency.UAH;
            personalPlan.MinSum = 100;
            personalPlan.MaxSum = 150_000;
            personalPlan.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            IWebElement conditions = FindElement(By.XPath("(//div[contains(.,'УСЛОВИЯ')])[6]"));
            conditions.Click();

            rate9MoWebEl = WaitUntilElementClickable(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[2]"));
            personalPlan.Rate9Months = MyConvertTo.StrWithDotAndPercentToDouble(rate9MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[3]"));
            personalPlan.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[4]"));
            personalPlan.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);
            rate24MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/div[1]/div[1]/div[1]/main[1]/section[3]/section[2]/div[1]/div[1]/div[2]/div[1]/table[2]/tbody[1]/tr[2]/td[5]"));
            personalPlan.Rate24Months = MyConvertTo.StrWithDotAndPercentToDouble(rate24MoWebEl.Text);

            personalPlan.IsAddable = true;
            personalPlan.IsWithdrawable = false;
            personalPlan.IsCancellable = false;
            personalPlan.Url = driver.Url;

            depositManager.SaveDeposit(personalPlan);

            WriteLogOnDepositCollected(bankName, personalPlan);
        }
    }
}