using System;

namespace BankAdviser.DAL.Entities
{
    public class ReplyEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BankName { get; set; }
        public string DepositName { get; set; }
        public double DepositRate { get; set; }
        public string DepositBonusInfo { get; set; }
        public double NetIncome { get; set; }
        public string Remark { get; set; }
        public string DepositUrl { get; set; }
    }
}