using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAdviser.WEB.Models
{
    public class ReplyVM
    {
        public int Id { get; set; }
        public int InquiryId { get; set; }
        public string BankName { get; set; }
        public string DepositName { get; set; }
        public double DepositInterest { get; set; }
        public string Commission { get; set; }
        public double NetProfit { get; set; }
        public string Bonuses { get; set; }
        public string DepositUrl { get; set; }
        public string Remark { get; set; }
    }
}