using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class BankManagerNModule : NinjectModule
    {
        public override void Load()
        {            
            Bind<IBankManager>().To<BankManager>();
        }
    }
}