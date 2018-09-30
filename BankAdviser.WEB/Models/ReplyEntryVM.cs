using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAdviser.WEB.Models
{
    
    public class ReplyEntryVM
    {
        private double depositRate;

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Банк")]
        public string BankName { get; set; }

        [Display(Name = "Депозит")]
        public string DepositName { get; set; }

        [Display(Name = "Ставка")]
        public double DepositRate { get; set; }

        [Display(Name = "")]
        public string DepositBonusInfo { get; set; }

        [Display(Name = "Доход")]
        public double NetIncome { get; set; }

        [Display(Name = "Рейтинг")]
        public int BankRating { get; set; }

        [Display(Name = "Размер")]
        public int BankAssetsRank { get; set; }

        [Display(Name = "")]
        public string Remark { get; set; }

        [Display(Name = "")]
        public string DepositUrl { get; set; }
    }
}