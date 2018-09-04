using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.WEB.Util
{
    public class NinjectRemMolule : NinjectModule
    {
        public override void Load()
        {            
            Bind<IReplyEntryManager>().To<ReplyEntryManager>();
        }
    }
}