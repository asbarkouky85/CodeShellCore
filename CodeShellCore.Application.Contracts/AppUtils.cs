using CodeShellCore.Data.Lookups;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class AppUtils
    {
        public static List<Named<object>> GetNamedList<T>(string prefix = "")
        {
            var lst = new List<Named<object>>();
            foreach (Enum cond in Enum.GetValues(typeof(T)))
            {
                lst.Add(new Named<object>
                {
                    Id = Convert.ToInt32(cond),
                    Name = prefix + EnumExtensions.GetString(cond)
                });
            }
            return lst;
        }
    }
}
