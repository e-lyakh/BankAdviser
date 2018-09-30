using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAdviser.WEB.Models
{
    public class RatingVM
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Банк")]
        public string BankName { get; set; }

        [Display(Name = "Общий рейтинг")]
        public double OverallRating { get; set; }

        [Display(Name = "Стрессо-устойчивость")]
        public double StressRating { get; set; }

        [Display(Name = "Лояльность вкладчиков")]
        public double LoyaltyRating { get; set; }

        [Display(Name = "Оценка аналитиков")]
        public double AnalystsRating { get; set; }

        [Display(Name = "Место по депозитам")]
        public double DepositRating { get; set; }
    }
}