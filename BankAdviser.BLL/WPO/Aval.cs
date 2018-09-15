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
            bankName = "ПриватБанк";
            pageUrl = "https://privatbank.ua/ru/depozit";

            bankId = GetBankId();
        }

        private int bankId;

        public override void CollectData ()
        {
            GoToUrl(pageUrl);

            // TODO collecting deposits data

            GetUahDeposits();

            Successor.CollectData();
        }

        private void GetUahDeposits()
        {
            // Стандарт срочный:

            DepositDTO standartSrochniy = new DepositDTO();

            standartSrochniy.BankId = bankId;            
            var test = FindElements(By.CssSelector("h3.deposit-name"));
            var test2 = FindElements(By.ClassName("contribution"));
            var test3 = FindElement(By.Id("body"));
            var test4 = FindChildElement(test3, By.XPath("./div[1]/div[1]/div[5]/div[1]/div[1]/h3[1]")).Text;
            standartSrochniy.Name = FindElements(By.CssSelector("h3.deposit-name"))[1].Text;
            standartSrochniy.Currency = Currency.UAH;
            standartSrochniy.MinSum = 0;
            standartSrochniy.MaxSum = double.MaxValue;           
            standartSrochniy.InterestsPeriodicity = InterestsPeriodicity.Monthly;
            
            standartSrochniy.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(FindElement(By.XPath("(//div[@class='deposit-percents__rate'])[5]")).Text);
            standartSrochniy.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(FindElement(By.XPath("(//div[@class='deposit-percents__rate'])[4]")).Text);
            standartSrochniy.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(FindElement(By.XPath("(//div[@class='deposit-percents__rate'])[3]")).Text);
            standartSrochniy.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(FindElement(By.XPath("(//div[@class='deposit-percents__rate'])[2]")).Text);
            standartSrochniy.Rate24Months = MyConvertTo.StrWithDotAndPercentToDouble(FindElement(By.XPath("(//div[@class='deposit-percents__rate'])[1]")).Text);

            standartSrochniy.IsAddable = FindElement(By.XPath("(//div[@class='col-c col4'])[4]")).Text == "Да" ? true : false;
            standartSrochniy.IsWithdrawable = false;
            standartSrochniy.IsCancellable = FindElement(By.XPath("(//div[contains(@class,'col-c col4')])[6]")).Text == "Да" ? true : false;

            standartSrochniy.BonusInfo = FindElement(By.XPath("(//div[contains(@class,'col-c col3')])[21]")).Text + "надбавка при пополнении";
            standartSrochniy.Url = driver.Url;

            depositManager.SaveDeposit(standartSrochniy);

            NotifyOnDepositCollected(bankName, standartSrochniy);
        }
    }
}