namespace BankAdviser.BLL.DTO
{
    public class DepositDTO
    {
        public int Id { get; set; }       
        public int BankId { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public string InterestsPeriodicity { get; set; }

        public double Rate1Weeks { get; set; }
        public double Rate2Weeks { get; set; }
        public double Rate1Months { get; set; }
        public double Rate2Months { get; set; }
        public double Rate3Months { get; set; }
        public double Rate4Months { get; set; }
        public double Rate6Months { get; set; }
        public double Rate7Months { get; set; }
        public double Rate9Months { get; set; }
        public double Rate12Months { get; set; }
        public double Rate13Months { get; set; }
        public double Rate18Months { get; set; }
        public double Rate24Months { get; set; }
        public double Rate36Months { get; set; }

        public bool IsAddable { get; set; }
        public bool IsWithdrawable { get; set; }
        public bool IsCancellable { get; set; }

        public string BonusInfo { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }

        public double GetRateByTerm(int months, int weeks = 0)
        {
            if (weeks == 1)
                return Rate1Weeks;
            if (weeks == 2)
                return Rate2Weeks;
            if (months == 1)
                return Rate1Months;
            if (months == 2)
                return Rate2Months;
            if (months == 3)
                return Rate3Months;
            if (months == 4)
                return Rate4Months;
            if (months == 6)
                return Rate6Months;
            if (months == 7)
                return Rate7Months;
            if (months == 9)
                return Rate9Months;
            if (months == 12)
                return Rate12Months;
            if (months == 13)
                return Rate13Months;
            if (months == 18)
                return Rate18Months;
            if (months == 24)
                return Rate24Months;
            if (months == 36)
                return Rate36Months;

            return 0;
        }
    }
}