using BankAdviser.BLL.Infrastructure;
using BankAdviser.Bot.Views;
using Ninject;
using Ninject.Modules;
using System.Windows;

namespace BankAdviser.Bot
{
    public partial class App : Application
    {
        //Dependencies injection:

        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            NinjectModule generalModule = new NjModule();
            container = new StandardKernel(generalModule);            
        }

        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();
        }        
    }
}