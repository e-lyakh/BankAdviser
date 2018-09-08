using BankAdviser.BLL.DTO;
using System.Collections.Generic;

namespace BankAdviser.BLL.Interfaces
{
    public interface IDepositManager
    {
        void SaveDeposit(DepositDTO depositDTO);
        IEnumerable<DepositDTO> SelectDeposits(int? iquiryId);
        void Dispose();
    }
}
