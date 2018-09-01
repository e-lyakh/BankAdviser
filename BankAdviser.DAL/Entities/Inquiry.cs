
using System;

namespace BankAdviser.DAL.Entities
{
    public class Inquiry
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
        public DateTime Date { get; set; }
        public string UserIP { get; set; }
    }

    public enum Currency
    {
        UAH,
        USD,
        EURO
    }
    public enum InterestPeriodicity
    {
        Weekly,
        Monthly,
        AtTheEnd
    }
    public enum BanksGroup
    {
        Private,
        State,
        Foreign
    }
}
