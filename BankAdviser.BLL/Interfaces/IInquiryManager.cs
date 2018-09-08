using BankAdviser.BLL.DTO;

namespace BankAdviser.BLL.Interfaces
{
    public interface IInquiryManager
    {
        int SaveInquiry(InquiryDTO inquiryDTO);
        InquiryDTO GetInquiry(int? inqId);
        void Dispose();
    }
}