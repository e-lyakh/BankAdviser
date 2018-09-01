
namespace BankAdviser.BLL.DTO
{
    public class InquiryDTO
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Sum { get; set; }
        public int Term { get; set; }
        public string InterestsPayout { get; set; }
        public bool IsAddable { get; set; }
        public bool IsWithdrawable { get; set; }
        public bool NeedPrivateBanks { get; set; }
        public bool NeedStateBanks { get; set; }
        public bool NeedForeignBanks { get; set; }
        public int BanksNum { get; set; }
    }
}
