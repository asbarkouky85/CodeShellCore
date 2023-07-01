using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Text.Globalization
{
    public class NumberWordConverter : ToWord
    {
        public NumberWordConverter(decimal number) : base(number, new CurrencyInfo(CurrencyInfo.Currencies.Empty), "", "", "", "") { }

        public static string ToArabic(int num)
        {
            NumberWordConverter con = new NumberWordConverter(Convert.ToDecimal(num));
            return con.ConvertToArabic();
        }
    }
}
