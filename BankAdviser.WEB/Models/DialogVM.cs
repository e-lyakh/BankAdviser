using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAdviser.WEB.Models
{
    public class DialogVM
    {
        public int Id { get; set; }
        public string Language { get; set; }        
        public string Currency { get; set; }
        public string Sum { get; set; }
        public string InterestPayout { get; set; }       
        public string Term { get; set; }
        public bool CanBeReplenished { get; set; }
        public bool IsPretermWithdrawable { get; set; }
        public string BanksGroup { get; set; }
        public string BanksNum { get; set; }
        public string Email { get; set; }
        public string Format { get; set; }
    }
}