using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
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

            logCollection = new ObservableCollection<LogEntry>();           
            logCollection.Add(new LogEntry
            {
                Time = DateTime.Now,
                Bank = "PUMB",
                Deposit = "Svobodniy",
                Currency = Currency.UAH,
                Term = 6,
                Status = "✓"
            });
            logCollection.Add(new LogEntry
            {
                Time = DateTime.Now,
                Bank = "Privat",
                Deposit = "Privat-Vklad",
                Currency = Currency.USD,
                Term = 12,
                Status = "✓"
            });

            IsRunBtnEnabled = true;
            IsStopBtnEnabled = false;
            gifVisibility = Visibility.Hidden;
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

        private ObservableCollection<LogEntry> logCollection;

        private int banksProcessed;
        private int depositsCollected;

        public event PropertyChangedEventHandler PropertyChanged;        

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
                        if (botManager != null && botManager.IsRunning)
                            botCancTokenSource.Cancel();

                        IsStopBtnEnabled = false;
                        IsRunBtnEnabled = true;
                        GifVisibility = Visibility.Hidden;
                    }));
            }
        }  
        
        public void OnDepositCollected(BankDTO bank, DepositDTO deposit)
        {
            mainWindow.Dispatcher.Invoke(() =>
            {
                if (deposit != null)
                    logCollection.Add(new LogEntry
                    {
                        Time = DateTime.Now,
                        Bank = bank.Name,
                        Deposit = deposit.Name,
                        Currency = deposit.Currency,
                        Term = deposit.GetTerm(),
                        Status = "✓"
                    });
                else
                    logCollection.Add(new LogEntry
                    {
                        Time = DateTime.Now,
                        Bank = "<Error>",
                        Status = "✗"
                    });
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
        public double Term { get; set; }
        public string Status { get; set; }
    }
}