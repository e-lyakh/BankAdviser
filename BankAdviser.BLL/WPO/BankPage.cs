using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using OpenQA.Selenium;
using System.Linq;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public abstract class BankPage : WebPage
    {
        public BankPage(IWebDriver driver, IBankManager bankManager, IDepositManager depositManager, DepositStatusHandler depositCollected)
                        : base(driver)
        {
            this.depositManager = depositManager;
            this.bankManager = bankManager;
            DepositCollected = depositCollected;
        }

        protected IBankManager bankManager;
        protected IDepositManager depositManager;

        protected string bankName;

        public event DepositStatusHandler DepositCollected;

        public BankPage Successor { get; set; }

        protected int GetBankId()
        {
            int id = bankManager.GetAll()
                    .Where(b => b.Name == bankName)
                    .LastOrDefault()
                    .Id;

            return id;
        }

        protected void NotifyOnDepositCollected(string bank, DepositDTO deposit)
        {
            DepositCollected?.Invoke(bank, deposit);
        }

        public abstract void CollectData();        
    }
}