using System;

namespace BankAdviser.BLL.DTO
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public DateTime SearchDate { get; set; }
        public string BankName { get; set; }
        public double OverallRating { get; set; }
        public double StressRating { get; set; }
        public double LoyaltyRating { get; set; }
        public double AnalystsRating { get; set; }
        public double DepositRating { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
    }
}
