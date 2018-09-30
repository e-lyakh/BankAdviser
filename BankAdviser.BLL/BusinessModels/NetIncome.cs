﻿using BankAdviser.BLL.DTO;
using BankAdviser.DAL.Entities;
using System;

namespace BankAdviser.BLL.BusinessModels
{
    public static class NetIncome
    {
        // https://investoriq.ru/banki/raschet-vklada-s-kapitalizaciej.html

        private const double revenueTax = 0.18;
        private const double militaryTax = 0.015;

        public static double Calculate(InquiryDTO inquiry, DepositDTO deposit)
        {
            double resultSum = 0;
            double taxSum = 0;
            double netSum = 0;

            double sum = inquiry.Sum;
            double rate = deposit.GetRateByTerm(inquiry.Term);
            int term = inquiry.Term;

            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Monthly)
            {
                resultSum = sum * Math.Pow((1 + rate / 100 / 12), term);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Quarterly)
            {
                resultSum = sum * Math.Pow((1 + rate / 100 / 4), term / 4);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.OnCompletion)
            {
                resultSum = sum * (1 + rate / 100 * term / 12);
            }

            taxSum = (resultSum - sum) * (revenueTax + militaryTax);
            netSum = resultSum - taxSum;
            return netSum;
        }

        public static double Calculate(Inquiry inquiry, Deposit deposit)
        {
            double resultSum = 0;
            double taxSum = 0;
            double netSum = 0;

            double sum = inquiry.Sum;
            double rate = deposit.GetRateByTerm(inquiry.Term);
            int term = inquiry.Term;

            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Monthly)
            {
                resultSum = sum * Math.Pow((1 + rate / 100 / 12), term);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.Quarterly)
            {
                resultSum = sum * Math.Pow((1 + rate / 100 / 4), term / 4);
            }
            if (inquiry.InterestsPeriodicity == InterestsPeriodicity.OnCompletion)
            {
                resultSum = sum * (1 + rate / 100 * term / 12);
            }

            taxSum = (resultSum - sum) * (revenueTax + militaryTax);
            netSum = resultSum - taxSum;
            return netSum;
        }
    }
}