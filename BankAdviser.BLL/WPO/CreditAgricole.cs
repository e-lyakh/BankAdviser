using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Entities;
using OpenQA.Selenium;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.WPO
{
    public class CreditAgricole : BankPage
    {
        public CreditAgricole(IWebDriver driver, IBankManager bankManager, IDepositManager depositManager, DepositStatusHandler depositCollected)
            : base(driver, bankManager, depositManager, depositCollected)
        {
            bankName = "Креди Агриколь";
            bankId = GetBankId(bankName);
        }

        private const string depositSrochniyUrl = "https://credit-agricole.ua/ru/privatnym-kliyentam/depoziti/depozit-strokovij";
        private const string depositNakopitelniyUrl = "https://credit-agricole.ua/ru/privatnym-kliyentam/depoziti/depozit-nakopichuvalnij";
        private const string depositEzhemesDoknodUrl = "https://credit-agricole.ua/ru/privatnym-kliyentam/depoziti/depozit-shchomisyachnij-dohid";
        private const string depositSberegatelniyUrl = "https://credit-agricole.ua/ru/privatnym-kliyentam/depoziti/depozit-oshchadnij";
                
        private IWebElement rate3MoWebEl;       
        private IWebElement rate6MoWebEl;
        private IWebElement rate12MoWebEl;
        private IWebElement rate15MoWebEl;
        private IWebElement rate18MoWebEl;
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
            // Srochniy:
            GoToUrl(depositSrochniyUrl);
            GetDepositSrochniy_Uah();
            GetDepositSrochniy_Usd_To50k();
            GetDepositSrochniy_Usd_From50k();

            // Nakopitelniy:
            GoToUrl(depositNakopitelniyUrl);
            GetDepositNakopitelniy_Uah();
            GetDepositNakopitelniy_Usd();

            // Ezhemesyachniy Doknod:
            GoToUrl(depositEzhemesDoknodUrl);
            GetEzhemesDoknod_Uah();
            GetEzhemesDoknod_Usd();

            // Sberegatelniy:
            GoToUrl(depositSberegatelniyUrl);
            GetSberegatelniy_Uah_To250k();
            GetSberegatelniy_Uah_From250k();
            GetSberegatelniy_Usd();
        }

        private void GetDepositSrochniy_Uah()
        {            
            DepositDTO srochniyUah = new DepositDTO();
            srochniyUah.BankId = bankId;
            srochniyUah.Name = "Срочный";
            srochniyUah.Currency = Currency.UAH;
            srochniyUah.MinSum = 5000;
            srochniyUah.MaxSum = double.MaxValue;
            srochniyUah.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate3MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[2]"));
            srochniyUah.Rate3Months = MyConvertTo.StrWithDotAndPercentToDouble(rate3MoWebEl.Text);
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[2]"));
            srochniyUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[2]"));
            srochniyUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[4]/td[2]"));
            srochniyUah.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);
            
            srochniyUah.IsAddable = false;
            srochniyUah.IsWithdrawable = false;
            srochniyUah.IsCancellable = false;
            srochniyUah.Url = driver.Url;

            depositManager.SaveDeposit(srochniyUah);

            WriteLogOnDepositCollected(bankName, srochniyUah);
        }
        private void GetDepositSrochniy_Usd_To50k()
        {
            DepositDTO srochniyUsdTo50k = new DepositDTO();
            srochniyUsdTo50k.BankId = bankId;
            srochniyUsdTo50k.Name = "Срочный";
            srochniyUsdTo50k.Currency = Currency.USD;
            srochniyUsdTo50k.MinSum = 1000;
            srochniyUsdTo50k.MaxSum = 49_999.99;
            srochniyUsdTo50k.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;
            
            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[3]"));
            srochniyUsdTo50k.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[3]"));
            srochniyUsdTo50k.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            srochniyUsdTo50k.IsAddable = false;
            srochniyUsdTo50k.IsWithdrawable = false;
            srochniyUsdTo50k.IsCancellable = false;
            srochniyUsdTo50k.Url = driver.Url;

            depositManager.SaveDeposit(srochniyUsdTo50k);

            WriteLogOnDepositCollected(bankName, srochniyUsdTo50k);
        }
        private void GetDepositSrochniy_Usd_From50k()
        {
            DepositDTO srochniyUsdTo50k = new DepositDTO();
            srochniyUsdTo50k.BankId = bankId;
            srochniyUsdTo50k.Name = "Срочный";
            srochniyUsdTo50k.Currency = Currency.USD;
            srochniyUsdTo50k.MinSum = 50_000;
            srochniyUsdTo50k.MaxSum = double.MaxValue;
            srochniyUsdTo50k.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate6MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[4]"));
            srochniyUsdTo50k.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[4]"));
            srochniyUsdTo50k.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);

            srochniyUsdTo50k.IsAddable = false;
            srochniyUsdTo50k.IsWithdrawable = false;
            srochniyUsdTo50k.IsCancellable = false;
            srochniyUsdTo50k.Url = driver.Url;

            depositManager.SaveDeposit(srochniyUsdTo50k);

            WriteLogOnDepositCollected(bankName, srochniyUsdTo50k);
        }

        private void GetDepositNakopitelniy_Uah()
        {
            DepositDTO nakopitelniyUah = new DepositDTO();
            nakopitelniyUah.BankId = bankId;
            nakopitelniyUah.Name = "Накопительный";
            nakopitelniyUah.Currency = Currency.UAH;
            nakopitelniyUah.MinSum = 5_000;
            nakopitelniyUah.MaxSum = 500_000;
            nakopitelniyUah.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[2]"));
            nakopitelniyUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[2]"));
            nakopitelniyUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate15MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[2]"));
            nakopitelniyUah.Rate15Months = MyConvertTo.StrWithDotAndPercentToDouble(rate15MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[4]/td[2]"));
            nakopitelniyUah.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            nakopitelniyUah.IsAddable = true;
            nakopitelniyUah.IsWithdrawable = false;
            nakopitelniyUah.IsCancellable = false;
            nakopitelniyUah.Url = driver.Url;

            depositManager.SaveDeposit(nakopitelniyUah);

            WriteLogOnDepositCollected(bankName, nakopitelniyUah);
        }
        private void GetDepositNakopitelniy_Usd()
        {
            DepositDTO nakopitelniyUsd = new DepositDTO();
            nakopitelniyUsd.BankId = bankId;
            nakopitelniyUsd.Name = "Накопительный";
            nakopitelniyUsd.Currency = Currency.USD;
            nakopitelniyUsd.MinSum = 1000;
            nakopitelniyUsd.MaxSum = 500_000;
            nakopitelniyUsd.InterestsPeriodicity = InterestsPeriodicity.OnCompletion;

            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[3]"));
            nakopitelniyUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[3]"));
            nakopitelniyUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate15MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[3]"));
            nakopitelniyUsd.Rate15Months = MyConvertTo.StrWithDotAndPercentToDouble(rate15MoWebEl.Text);

            nakopitelniyUsd.IsAddable = true;
            nakopitelniyUsd.IsWithdrawable = false;
            nakopitelniyUsd.IsCancellable = false;
            nakopitelniyUsd.Url = driver.Url;

            depositManager.SaveDeposit(nakopitelniyUsd);

            WriteLogOnDepositCollected(bankName, nakopitelniyUsd);
        }

        private void GetEzhemesDoknod_Uah()
        {
            DepositDTO ezhemesDoknodUah = new DepositDTO();
            ezhemesDoknodUah.BankId = bankId;
            ezhemesDoknodUah.Name = "Ежемесячный доход";
            ezhemesDoknodUah.Currency = Currency.UAH;
            ezhemesDoknodUah.MinSum = 5_000;
            ezhemesDoknodUah.MaxSum = double.MaxValue;
            ezhemesDoknodUah.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[2]"));
            ezhemesDoknodUah.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[2]"));
            ezhemesDoknodUah.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[2]"));
            ezhemesDoknodUah.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            ezhemesDoknodUah.IsAddable = false;
            ezhemesDoknodUah.IsWithdrawable = false;
            ezhemesDoknodUah.IsCancellable = false;
            ezhemesDoknodUah.Url = driver.Url;

            depositManager.SaveDeposit(ezhemesDoknodUah);

            WriteLogOnDepositCollected(bankName, ezhemesDoknodUah);
        }
        private void GetEzhemesDoknod_Usd()
        {
            DepositDTO ezhemesDoknodUsd = new DepositDTO();
            ezhemesDoknodUsd.BankId = bankId;
            ezhemesDoknodUsd.Name = "Ежемесячный доход";
            ezhemesDoknodUsd.Currency = Currency.USD;
            ezhemesDoknodUsd.MinSum = 1_000;
            ezhemesDoknodUsd.MaxSum = double.MaxValue;
            ezhemesDoknodUsd.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rate6MoWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[3]"));
            ezhemesDoknodUsd.Rate6Months = MyConvertTo.StrWithDotAndPercentToDouble(rate6MoWebEl.Text);
            rate12MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[2]/td[3]"));
            ezhemesDoknodUsd.Rate12Months = MyConvertTo.StrWithDotAndPercentToDouble(rate12MoWebEl.Text);
            rate18MoWebEl = FindElement(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[3]/td[3]"));
            ezhemesDoknodUsd.Rate18Months = MyConvertTo.StrWithDotAndPercentToDouble(rate18MoWebEl.Text);

            ezhemesDoknodUsd.IsAddable = false;
            ezhemesDoknodUsd.IsWithdrawable = false;
            ezhemesDoknodUsd.IsCancellable = false;
            ezhemesDoknodUsd.Url = driver.Url;

            depositManager.SaveDeposit(ezhemesDoknodUsd);

            WriteLogOnDepositCollected(bankName, ezhemesDoknodUsd);
        }

        private void GetSberegatelniy_Uah_To250k()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Сберегательный";
            deposit.Currency = Currency.UAH;
            deposit.MinSum = 1_000;
            deposit.MaxSum = 249_999.99;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rateTermlessWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[2]"));
            deposit.RateTermless = MyConvertTo.StrWithDotAndPercentToDouble(rateTermlessWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = true;
            deposit.IsCancellable = true;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetSberegatelniy_Uah_From250k()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Сберегательный";
            deposit.Currency = Currency.UAH;
            deposit.MinSum = 250_000;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rateTermlessWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[3]"));
            deposit.RateTermless = MyConvertTo.StrWithDotAndPercentToDouble(rateTermlessWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = true;
            deposit.IsCancellable = true;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
        private void GetSberegatelniy_Usd()
        {
            DepositDTO deposit = new DepositDTO();
            deposit.BankId = bankId;
            deposit.Name = "Сберегательный";
            deposit.Currency = Currency.USD;
            deposit.MinSum = 100;
            deposit.MaxSum = double.MaxValue;
            deposit.InterestsPeriodicity = InterestsPeriodicity.Monthly;

            rateTermlessWebEl = WaitElementIfExists(By.XPath("/html[1]/body[1]/main[1]/section[2]/div[1]/div[1]/div[7]/table[1]/tbody[1]/tr[1]/td[4]"));
            deposit.RateTermless = MyConvertTo.StrWithDotAndPercentToDouble(rateTermlessWebEl.Text);

            deposit.IsAddable = true;
            deposit.IsWithdrawable = true;
            deposit.IsCancellable = true;
            deposit.Url = driver.Url;

            depositManager.SaveDeposit(deposit);

            WriteLogOnDepositCollected(bankName, deposit);
        }
    }
}