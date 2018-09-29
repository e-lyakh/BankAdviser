using BankAdviser.BLL.Interfaces;
using BankAdviser.BLL.Services;
using BankAdviser.DAL.Interfaces;
using BankAdviser.DAL.Repositories;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class NjModule : NinjectModule
    {
        private string connectionString;
        public NjModule()
        {
        }
        public NjModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<IBankManager>().To<BankManager>();            
            Bind<IDepositManager>().To<DepositManager>();
            Bind<IInquiryManager>().To<InquiryManager>();
            Bind<IReplyEntryManager>().To<ReplyEntryManager>();

            Bind<IBotManager>().To<BotManager>();
        }
    }
}