using BankAdviser.DAL.Interfaces;
using OpenQA.Selenium;

namespace BankAdviser.BLL.WPO
{
    public class PrivatBankPage : WebPage
    {
        public PrivatBankPage(IWebDriver driver, IUnitOfWork uow) : base(driver)
        {
            this.uow = uow;

            baseUrl = "https://privatbank.ua";
        }

        private IUnitOfWork uow;

        private readonly string depozitUrl = "https://privatbank.ua/ru/depozit";

        public void CollectDepositsData ()
        {

        }
    }
}