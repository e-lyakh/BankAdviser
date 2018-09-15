using BankAdviser.BLL.Interfaces;
using BankAdviser.Bot.ViewModels;
using System.Windows;

namespace BankAdviser.Bot.Views
{
    public partial class MainWindow : Window
    {
        private BotViewModel vm;
       
        public MainWindow(IBotManager botManager)
        {
            InitializeComponent();
            
            vm = new BotViewModel(this, botManager);

            DataContext = vm;

            botManager.DepositCollected += vm.OnDepositCollected;
            botManager.OnAllWorkDone += vm.OnAllWorkDone;
            vm.OnLogUpdated += ScrollDownLog;
        }

        private void ScrollDownLog()
        {
            if (dgLog.Items.Count > 0)
                dgLog.ScrollIntoView(dgLog.Items[dgLog.Items.Count - 1]);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            vm.OnWindowClosed();
        }
    }
}