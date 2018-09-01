using BankAdviser.DAL.Interfaces;
using BankAdviser.DAL.Repositories;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule()
        {            
        }
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            //Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}