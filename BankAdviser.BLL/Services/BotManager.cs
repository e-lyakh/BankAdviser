using BankAdviser.BLL.DTO;
using BankAdviser.BLL.Interfaces;
using BankAdviser.DAL.Entities;
using BankAdviser.DAL.Interfaces;
using System.Threading;

namespace BankAdviser.BLL.Services
{
    public class BotManager : IBotManager
    {
        public BotManager(IUnitOfWork uow)
        {
            db = uow;
        }

        private IUnitOfWork db;

        public delegate void DepositStatusHandler(BankDTO bank, DepositDTO deposit);
        public event DepositStatusHandler DepositCollected;

        public CancellationToken CancToken { get; set; }
        public bool IsRunning { get; set; }

        public void Run()
        {
            IsRunning = true;

            // TODO

            // Test
            DepositDTO deposit = new DepositDTO()
            {
                Name = "Deposit-X",
                Currency = Currency.EUR,
                Rate6Months = 0.1
            };
            BankDTO bank = new BankDTO()
            {
                Name = "Bank-X"
            };
            DepositCollected?.Invoke(bank, deposit);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}