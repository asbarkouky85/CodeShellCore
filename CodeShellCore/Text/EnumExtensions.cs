using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text
{
    public static class EnumExtensions
    {
        public static string GetString(this Enum @enum)
        {
            return Strings.Word(@enum.GetType().Name + "_" + @enum.ToString());
        }

        public static string StringFormat(this Enum @enum)
        {
            return @enum.GetType().Name + "_" + @enum.ToString();
        }
    }
}
