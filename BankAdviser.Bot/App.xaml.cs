using BankAdviser.BLL.Infrastructure;
using BankAdviser.Bot.Util;
using BankAdviser.Bot.Views;
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
            //container = new StandardKernel();
            //container.Bind<IBotManager>().To<BotManager>().InTransientScope();

            NinjectModule uowModule = new UowNModule();
            NinjectModule bmModule = new BotManagerNModule();            
            container = new StandardKernel(uowModule, bmModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(container));
        }

        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();
        }        
    }
}