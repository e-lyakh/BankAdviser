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

        public double NetIncome { get; set; }

        public double GetTerm()
        {
            if (Rate1Weeks > 0)
                return 0.25;
            if (Rate2Weeks > 0)
                return 0.5;
            if (Rate1Months > 0)
                return 1;
            if (Rate2Months > 0)
                return 2;
            if (Rate3Months > 0)
                return 3;
            if (Rate4Months > 0)
                return 4;
            if (Rate6Months > 0)
                return 6;
            if (Rate7Months > 0)
                return 7;
            if (Rate9Months > 0)
                return 9;
            if (Rate12Months > 0)
                return 12;
            if (Rate13Months > 0)
                return 13;
            if (Rate18Months > 0)
                return 18;
            if (Rate24Months > 0)
                return 24;
            if (Rate36Months > 0)
                return 36;

            return 0;
        }

        public bool HasTerm(int months, int weeks = 0)
        {
            if (weeks == 1 && Rate1Weeks > 0)
                return true;
            if (weeks == 2 && Rate2Weeks > 0)
                return true;
            if (months == 1 && Rate1Months > 0)
                return true;
            if (months == 2 && Rate2Months > 0)
                return true;
            if (months == 3 && Rate3Months > 0)
                return true;
            if (months == 4 && Rate4Months > 0)
                return true;
            if (months == 6 && Rate6Months > 0)
                return true;
            if (months == 7 && Rate7Months > 0)
                return true;
            if (months == 9 && Rate9Months > 0)
                return true;
            if (months == 12 && Rate12Months > 0)
                return true;
            if (months == 13 && Rate13Months > 0)
                return true;
            if (months == 18 && Rate18Months > 0)
                return true;
            if (months == 24 && Rate24Months > 0)
                return true;
            if (months == 36 && Rate36Months > 0)
                return true;

            return false;
        }

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