using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Services
{
    public static class MyConvertTo
    {
        public static double StrWithDotToDouble(string text)
        {
            double result;

            if (text.Contains("."))
                result = Convert.ToDouble(text.Replace('.', ','));           
            else
                result = Convert.ToDouble(text);

            return result;
        }

        public static double StrWithDotAndPercentToDouble(string text)
        {
            double result;

            text = text.Substring(0, text.Length - 2);

            if (text.Contains("."))
                result = Convert.ToDouble(text.Replace('.', ','));
            else
                result = Convert.ToDouble(text);

            return result;
        }
    }
}