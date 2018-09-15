using BankAdviser.BLL.Infrastructure;
using BankAdviser.Bot.Views;
using BankAdviser.DAL.Services;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
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
            //NinjectModule uowModule = new UowNModule();
            ////NinjectModule uowModule = new UowNModule(Settings.ConnectionString);
            //NinjectModule botManModule = new BotManagerNModule();
            //NinjectModule depositManModule = new DepositManagerNModule();
            //NinjectModule bankManModule = new BankManagerNModule();
            //container = new StandardKernel(uowModule, botManModule, depositManModule, bankManModule);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(container));

            //NinjectModule generalModule = new NjModule();
            NinjectModule generalModule = new NjModule("DefaultConnection");
            //container = new StandardKernel(generalModule);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(container));

            container = new StandardKernel(generalModule);
            //container.Bind<IBotManager>().To<BotManager>().InTransientScope();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();
        }        
    }
}