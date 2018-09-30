using BankAdviser.DAL.Entities;
using System;

namespace BankAdviser.BLL.BusinessModels
{
    public class CalculatorBM
    {
        private const double revenueTax = 0.18;
        private const double militaryTax = 0.015;

        public double Sum { get; set; }
        public double Rate { get; set; }
        public double Term { get; set; }
        public string InterestsPeriodicity { get; set; }
        public double ResultSum { get; set; }
        public double TaxSum { get; set; }
        public double NetSum { get; set; }

        public void Calculate()
        {
            if (InterestsPeriodicity == DAL.Entities.InterestsPeriodicity.Monthly)
            {
                ResultSum = Sum * Math.Pow((1 + Rate / 100 / 12), Term);                
            }
            if (InterestsPeriodicity == DAL.Entities.InterestsPeriodicity.OnCompletion)
            {
                ResultSum = Sum * (1 + Rate / 100 * Term / 12);
            }
            TaxSum = (ResultSum - Sum) * (revenueTax + militaryTax);
            NetSum = ResultSum - TaxSum;
        }
    }
}