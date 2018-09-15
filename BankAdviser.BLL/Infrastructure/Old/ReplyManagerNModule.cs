using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class ReplyManagerNModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReplyEntryManager>().To<ReplyEntryManager>();
        }
    }
}
