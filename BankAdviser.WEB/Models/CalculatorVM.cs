using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAdviser.WEB.Models
{
    [Bind(Exclude = "ValidationErrors")]
    public class CalculatorVM
    {
        [Required]
        [Range(0, 1000000000)]
        public double Sum { get; set; }
        [Required]
        [Range(1, 100)]
        public double Rate { get; set; }
        [Required]
        [Range(1, 36)]
        public double Term { get; set; }
        public string InterestsPeriodicity { get; set; }
        public double ResultSum { get; set; }
        public double TaxSum { get; set; }
        public double NetSum { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; }
    }
}