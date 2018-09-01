
using System;

namespace BankAdviser.DAL.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public int InquiryId { get; set; }
        public int DepositInfoId { get; set; }
        public double NetProfit { get; set; }
        public string Bonuses { get; set; }
        public string DepositUrl { get; set; }
        public string Remark { get; set; }
    }
}