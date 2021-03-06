﻿using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text
{
    public static class EnumExtensions
    {
        public static string GetString(this Enum @enum)
        {
            return @enum.GetType().Name + "_" + @enum.ToString();
        }

        public static string StringFormat(this Enum @enum)
        {
            return @enum.GetType().Name + "_" + @enum.ToString();
        }


        public static string ToEnumString<T>(this long value)
        {
            var t = typeof(T);
            var x = Enum.GetName(t, value);
            return t.Name + "_" + x;
        }

        public static string ToEnumString<T>(this int value)
        {
            var t = typeof(T);
            var x = Enum.GetName(t, value);
            return t.Name + "_" + x;
        }

    }
}
