using BankAdviser.DAL.Interfaces;
using BankAdviser.DAL.Repositories;
using Ninject.Modules;

namespace BankAdviser.BLL.Infrastructure
{
    public class NinjectUowModule : NinjectModule
    {
        private string connectionString;
        public NinjectUowModule()
        {            
        }
        public NinjectUowModule(string connection)
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