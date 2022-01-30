using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Text.Localization
{
    public class Language
    {
        public CultureInfo Culture { get; private set; }

        public Language()
        {
            Culture = Shell.DefaultCulture;
        }

        public void SetCulture(string code)
        {
            CultureInfo inf = new CultureInfo(code);
            if (inf != null)
                Culture = inf;
        }

        public static Language Default
        {
            get
            {
                Language l = new Language();
                l.SetCulture(Shell.DefaultCulture.TwoLetterISOLanguageName);
                return l;
            }
        }

    }
}
