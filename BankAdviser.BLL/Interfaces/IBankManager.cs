using BankAdviser.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Interfaces
{
    public interface IBankManager
    {
        void SaveBank(BankDTO bankDTO);
        BankDTO GetBank(int? bankId);
        Dictionary<DepositDTO, BankDTO> GetBanksByDeposits(IEnumerable<DepositDTO> deposits);
        void Dispose();
    }
}