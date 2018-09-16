using System.Threading;
using System.Threading.Tasks;
using static BankAdviser.BLL.Services.BotManager;

namespace BankAdviser.BLL.Interfaces
{
    public interface IBotManager
    {
        void Run();

        CancellationToken CancToken { get; set; }
        bool IsRunning { get; set; }
        
        event DepositStatusHandler DepositCollected;
        event AllWorkDoneNotifier OnAllWorkDone;
    }
}
