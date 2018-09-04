namespace BankAdviser.WEB.Models
{
    public class EnquiryVM
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Sum { get; set; }
        public int Term { get; set; }
        public string InterestsPeriodicity { get; set; }
        public bool IsAddable { get; set; }
        public bool IsWithdrawable { get; set; }
        public bool IsCancellable { get; set; }
        public bool ArePrivateBanksIncluded { get; set; }
        public bool AreStateBanksIncluded { get; set; }
        public bool AreForeignBanksIncluded { get; set; }
        public int BanksNum { get; set; }
        public string UserIP { get; set; }
    }
}