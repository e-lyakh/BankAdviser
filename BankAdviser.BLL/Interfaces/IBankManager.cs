using BankAdviser.BLL.DTO;
using System.Collections.Generic;

namespace BankAdviser.BLL.Interfaces
{
    public interface IBankManager
    {
        void SaveBank(BankDTO bankDTO);
        BankDTO GetBank(int? bankId);
        IEnumerable<BankDTO> GetAll();
        void Dispose();
    }
}