using System;

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

            if (text != null && text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);

                if (text.Contains("."))
                    result = Convert.ToDouble(text.Replace('.', ','));
                else
                    result = Convert.ToDouble(text);

                return result;
            }
            else
                return -1;            
        }
    }
}