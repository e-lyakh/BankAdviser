using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAdviser.WEB.Models
{
    [Bind (Exclude = "Id, UserIP, ValidationErrors")]
    public class InquiryVM
    {
        public int Id { get; set; }        
        public string Currency { get; set; }
        [Required]
        [Range(0, 1000000000)]
        public double Sum { get; set; }
        [Required]
        [Range(1, 36)]
        public int Term { get; set; }
        public string InterestsPeriodicity { get; set; }
        public bool IsAddable { get; set; }
        public bool IsWithdrawable { get; set; }
        public bool IsCancellable { get; set; }
        public bool ArePrivateBanksIncluded { get; set; }
        public bool AreStateBanksIncluded { get; set; }
        public bool AreForeignBanksIncluded { get; set; }
        public int BanksNum { get; set; }
        public string SortOrder { get; set; }
        public string UserIP { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; }
    }
}