using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class InquiryManagerNModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInquiryManager>().To<InquiryManager>();
        }
    }
}