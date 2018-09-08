using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.WEB.Util
{
    public class NinjectEmMolule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInquiryManager>().To<InquiryManager>();            
        }
    }
}