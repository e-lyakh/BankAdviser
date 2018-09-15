using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class DepositManagerNModule : NinjectModule
    {
        public override void Load()
        {            
            Bind<IDepositManager>().To<DepositManager>();
        }
    }
}