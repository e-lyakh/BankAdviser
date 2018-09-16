using BankAdviser.BLL.DTO;
using BankAdviser.DAL.Entities;
using System;

namespace BankAdviser.BLL.BusinessModels
{
    public static class NetIncome
    {
        private const double revenueTax = 0.18;
        private const double militaryTax = 0.015;

        public static double Calculate(InquiryDTO inquiry, DepositDTO deposit)
        {
            // https://investoriq.ru/banki/raschet-vklada-s-kapitalizaciej.html

            double netIncome = 0;

            double sum = inquiry.Sum;
            double rate = deposit.GetRateByTerm(inquiry.Term);
            int term = inquiry.Term;

            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Monthly)
            {
                return sum * (rate / 12 / 100 * term) * (1 - revenueTax - militaryTax);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Quarterly)
            {
                return (sum * Math.Pow((1 + rate / 4 / 100), term) - sum) * (1 - revenueTax - militaryTax);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.OnCompletion)
            {
                return (sum * Math.Pow((1 + rate / 12 / 100), term) - sum) * (1 - revenueTax - militaryTax);
            }

            return netIncome;
        }

        public static double Calculate(Inquiry inquiry, Deposit deposit)
        {
            // https://investoriq.ru/banki/raschet-vklada-s-kapitalizaciej.html

            double netIncome = 0;

            double sum = inquiry.Sum;
            double rate = deposit.GetRateByTerm(inquiry.Term);
            int term = inquiry.Term;

            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Monthly)
            {
                return sum * (rate / 12 / 100 * term) * (1 - revenueTax - militaryTax);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Quarterly)
            {
                return (sum * Math.Pow((1 + rate / 4 / 100), term) - sum) * (1 - revenueTax - militaryTax);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.OnCompletion)
            {
                return (sum * Math.Pow((1 + rate / 12 / 100), term) - sum) * (1 - revenueTax - militaryTax);
            }

            return netIncome;
        }
    }
}