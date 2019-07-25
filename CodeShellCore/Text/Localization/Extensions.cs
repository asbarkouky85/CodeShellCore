using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Text.Localization
{
    public static class Extensions
    {
        public static string HijriToGreg(this string date, string inFormat, string outFormat= "yyyy-MM-dd")
        {
            try
            {
                CultureInfo arCI = new CultureInfo("ar-SA");
                DateTime tempDate = DateTime.ParseExact(date, inFormat, arCI.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                return tempDate.ToString(outFormat);
            }
            catch
            {
                DateTime dt;
                if (DateTime.TryParse(date, out dt))
                    return dt.ToString(outFormat);
                return null;
            }
            
        }


    }
}
