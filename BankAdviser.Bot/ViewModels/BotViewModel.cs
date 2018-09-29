using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Infrastructure;
using BankAdviser.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BankAdviser.Bot.ViewModels
{
    public class BotViewModel : INotifyPropertyChanged
    {
        public BotViewModel (Window mainWindow, IBotManager botManager)
        {
            this.mainWindow = mainWindow;
            this.botManager = botManager;

            IsRunBtnEnabled = true;
            IsStopBtnEnabled = false;

            gifVisibility = Visibility.Hidden;

            isBrowserMinimized = BotSettings.IsBrowserMinimized;

            logCollection = new ObservableCollection<LogEntry>();            
        }

        private Window mainWindow;
        private IBotManager botManager;

        private Task botTask;
        private CancellationTokenSource botCancTokenSource;
        private CancellationToken botCancToken;

        private RelayCommand runCommand;
        private RelayCommand stopCommand;

        private bool isRunBtnEnabled;
        private bool isStopBtnEnabled;

        private Visibility gifVisibility;

        private bool isBrowserMinimized;

        private ObservableCollection<LogEntry> logCollection;

        private int banksProcessed;
        private int depositsCollected;

        private string currentBank;

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ScrollDownHandler();
        public event ScrollDownHandler OnLogUpdated;

        public bool IsRunBtnEnabled
        {
            get
            {
                return isRunBtnEnabled;
            }
            set
            {
                isRunBtnEnabled = value;
                OnPropertyChanged("IsRunBtnEnabled");
            }
        }
        public bool IsStopBtnEnabled
        {
            get
            {
                return isStopBtnEnabled;
            }
            set
            {
                isStopBtnEnabled = value;
                OnPropertyChanged("IsStopBtnEnabled");
            }
        }
        public Visibility GifVisibility
        {
            get
            {
                return gifVisibility;
            }
            set
            {
                gifVisibility = value;
                OnPropertyChanged("GifVisibility");
            }
        }
        public bool IsBrowserMinimized
        {
            get
            {
                return isBrowserMinimized;
            }
            set
            {
                isBrowserMinimized = value;
                OnPropertyChanged("IsBrowserMinimized");
                BotSettings.IsBrowserMinimized = value;
            }
        }
        public ObservableCollection<LogEntry> LogCollection
        {
            get
            {
                return logCollection;
            }
            set
            {
                logCollection = value;
                OnPropertyChanged("LogCollection");
            }
        }
        public int BanksProcessed
        {
            get
            {
                return banksProcessed;
            }
            set
            {
                banksProcessed = value;
                OnPropertyChanged("BanksProcessed");
            }
        }
        public int DepositsCollected
        {
            get
            {
                return depositsCollected;
            }
            set
            {
                depositsCollected = value;
                OnPropertyChanged("DepositsCollected");
            }
        }

        public RelayCommand RunCommand
        {
            get
            {
                return runCommand ??
                    (runCommand = new RelayCommand(obj =>
                    {
                        IsRunBtnEnabled = false;
                        IsStopBtnEnabled = true;
                        GifVisibility = Visibility.Visible;

                        botTask = new Task(botManager.Run);
                        botCancTokenSource = new CancellationTokenSource();
                        botCancToken = botCancTokenSource.Token;
                        botManager.CancToken = botCancToken;
                        botTask.Start();
                    }));
            }
        }
        public RelayCommand StopCommand
        {
            get
            {
                return stopCommand ??
                    (stopCommand = new RelayCommand(obj =>
                    {
                        IsStopBtnEnabled = false;
                        IsRunBtnEnabled = true;
                        GifVisibility = Visibility.Hidden;

                        if (botManager != null && botManager.IsRunning)
                            botCancTokenSource.Cancel();
                    }));
            }
        }  
        
        public void OnDepositCollected(string bank, DepositDTO deposit)
        {
            mainWindow.Dispatcher.Invoke(() =>
            {
                if (deposit != null)
                {
                    double? maxTerm = null;
                    if (deposit.GetMaxTerm() < 37)
                        maxTerm = deposit.GetMaxTerm();
                    
                    logCollection.Add(new LogEntry
                    {
                        Time = DateTime.Now,
                        Bank = bank,
                        Deposit = deposit.Name,
                        Currency = deposit.Currency,
                        MaxTerm = maxTerm,
                        IntPeriodicity = deposit.InterestsPeriodicity,
                        Status = "✓"
                    });

                    DepositsCollected++;

                    if (currentBank == null)
                        currentBank = bank;
                    else if (bank != currentBank)
                    {
                        currentBank = bank;
                        BanksProcessed++;
                    }
                }                    
                else
                {
                    logCollection.Add(new LogEntry
                    {
                        Time = DateTime.Now,
                        Bank = "<Error>",
                        Status = "✗"
                    });
                }

                OnLogUpdated?.Invoke();
            });
        }

        public void OnAllWorkDone()
        {            
            mainWindow.Dispatcher.Invoke(() =>
            {
                BanksProcessed++;

                logCollection.Add(new LogEntry
                {
                    Time = DateTime.Now,
                    Bank = "All work done"
                });

                OnLogUpdated?.Invoke();

                IsStopBtnEnabled = false;
                IsRunBtnEnabled = true;
                GifVisibility = Visibility.Hidden;
            });
        }

        public void OnWindowClosed()
        {
            if (botManager != null && botManager.IsRunning)
                botCancTokenSource.Cancel();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class LogEntry
    {
        public DateTime Time { get; set; }
        public string Bank { get; set; }
        public string Deposit { get; set; }
        public string Currency {get; set; }
        public double? MaxTerm { get; set; }
        public string IntPeriodicity { get; set; }
        public string Status { get; set; }
    }
}