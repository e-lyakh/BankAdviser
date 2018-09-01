using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using Ninject.Modules;

namespace BankAdviser.WEB.Util
{
    public class RequestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInquiryService>().To<InquiryService>();
        }
    }
}