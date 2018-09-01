using BankAdviser.BLL.DTO;
using System.Collections.Generic;

namespace BankAdviser.BLL.Interfaces
{
    public interface IInquiryService
    {
        void SaveInquiry(InquiryDTO inquiryDto);
        DialogDTO GetDialog(int? id);
        IEnumerable<DialogDTO> GetDialogs();
        void Dispose();
    }
}
