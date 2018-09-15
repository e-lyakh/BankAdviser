using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
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
            pageUrl = "https://retail.pumb.ua/ru/deposit";
        }

        public override void CollectData ()
        {
            GoToUrl(pageUrl);

            // TODO collecting deposits data
            GetDepDokhodiyData();

            Successor.CollectData();
        }

        private void GetDepDokhodiyData()
        {
            DepositDTO dokhodniyUah = new DepositDTO();

            IWebElement goToLink = FindElement(By.XPath("/html[1]/body[1]/section[1]/div[2]/div[1]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]/div[1]/div[1]"));
            goToLink.Click();

            depositManager.SaveDeposit(dokhodniyUah);

            NotifyOnDepositCollected(bankName, dokhodniyUah);
        }
    }
}