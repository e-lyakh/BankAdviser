using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.Bot.Util
{
    public class BotManagerNModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBotManager>().To<BotManager>();           
        }
    }
}