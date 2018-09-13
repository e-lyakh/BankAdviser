using BankAdviser.DAL.Interfaces;
using BankAdviser.DAL.Repositories;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class UowNModule : NinjectModule
    {
        private string connectionString;
        public UowNModule()
        {            
        }
        public UowNModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            //Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}