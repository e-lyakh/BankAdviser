using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.WPO;
using BankAdviser.DAL.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Services
{
    public class BotManager : IBotManager
    {
        public BotManager(IUnitOfWork uow, IBankManager bankManager, IDepositManager depositManager)
        {
            this.uow = uow;
            this.bankManager = bankManager;
            this.depositManager = depositManager;
        }

        private IUnitOfWork uow;
        private IBankManager bankManager;
        private IDepositManager depositManager;
        
        private IWebDriver chromeDriver;
        private IWebDriver ffDriver;

        private Oshad oshad;        
        private Pumb pumb;
        private Aval aval;
        private Ukrsib ukrsib;
        private CreditAgricole creditAgricole;        

        public static bool IsMinimized { get; set; }

        public delegate void DepositStatusHandler(string bank, DepositDTO deposit);
        public event DepositStatusHandler DepositCollected;

        public delegate void AllWorkDoneNotifier();
        public event AllWorkDoneNotifier OnAllWorkDone;

        public CancellationToken CancToken { get; set; }
        public bool IsRunning { get; set; }

        private IWebDriver GetChromeDriver()
        {
            if (chromeDriver == null)                          
                chromeDriver = new ChromeDriver();

            if (BotSettings.IsBrowserMinimized)
                chromeDriver.Manage().Window.Minimize();

            return chromeDriver;
        }
        private IWebDriver GetFfDriver()
        {
            if (ffDriver == null)
                ffDriver = new FirefoxDriver();

            if (BotSettings.IsBrowserMinimized)
                ffDriver.Manage().Window.Minimize();

            return ffDriver;
        }

        public void Run()
        {
            IsRunning = true;

            chromeDriver = GetChromeDriver();

            WebDriverManager.MinimizeChromeDriverWndAsync();

            oshad = new Oshad(chromeDriver, bankManager, depositManager, DepositCollected);
            pumb = new Pumb(chromeDriver, bankManager, depositManager, DepositCollected);
            aval = new Aval(chromeDriver, bankManager, depositManager, DepositCollected);
            ukrsib = new Ukrsib(chromeDriver, bankManager, depositManager, DepositCollected);
            creditAgricole = new CreditAgricole(chromeDriver, bankManager, depositManager, DepositCollected);

            oshad.Successor = pumb;
            pumb.Successor = aval;
            aval.Successor = ukrsib;
            ukrsib.Successor = creditAgricole;
            creditAgricole.Successor = null;

            oshad.CollectData();

            OnAllWorkDone?.Invoke();

            oshad.QuitDriver();
        }

        public Task WaitForStopAsync()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    if(CancToken.IsCancellationRequested)
                    {
                        if (pumb.IsDriverRunning)
                        {
                            DepositCollected?.Invoke(pumb.BankName, null);
                            pumb.QuitDriver();
                        }                            
                        else if (aval.IsDriverRunning)
                        {
                            DepositCollected?.Invoke(pumb.BankName, null);
                            aval.QuitDriver();
                        }                            

                        uow.Dispose();
                    }
                }
            });
        }
    }
}