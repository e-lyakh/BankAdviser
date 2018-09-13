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
            botManager.DepositCollected += vm.OnDepositCollected;

            DataContext = vm;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            vm.OnWindowClosed();
        }
    }
}