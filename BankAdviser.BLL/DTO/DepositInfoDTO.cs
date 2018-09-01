using System;

namespace BankAdviser.BLL.DTO
{
    public class DepositInfoDTO
    {
        public int Id { get; set; }
        public DateTime SearchDate { get; set; }
        public string BankId { get; set; }
        public string DepositName { get; set; }
        public string Currency { get; set; } // UAH / USD / EUR
        public double MinSum { get; set; }
        public bool IsAddable { get; set; }
        public bool IsWithdrawable { get; set; }
        public bool IsCancellable { get; set; }
        public string InterestsPayout { get; set; } // Monthly / At the end of a term

        public double Rate1Mo { get; set; }
        public double Rate3Mos { get; set; }
        public double Rate6Mos { get; set; }
        public double Rate9Mos { get; set; }
        public double Rate12Mos { get; set; }

        public string BonusInfo { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
    }
}