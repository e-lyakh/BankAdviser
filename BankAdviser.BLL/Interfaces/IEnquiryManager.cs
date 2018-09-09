using BankAdviser.BLL.DTO;

namespace BankAdviser.BLL.Interfaces
{
    public interface IEnquiryManager
    {
        int SaveEnquiry(EnquiryDTO enquiryDTO);
        EnquiryDTO GetEnquiry(int? enqId);
        void Dispose();
    }
}