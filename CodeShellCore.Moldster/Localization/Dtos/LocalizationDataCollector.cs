using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Localization.Dtos
{
    public enum StringType { Word, Column, Page, Message }
    public class LocalizationDataCollector
    {
        public List<string> Words = new List<string>();
        public List<string> Messages = new List<string>();
        public List<string> Columns = new List<string>();
        public List<string> Pages = new List<string>();

        public LocalizationDataCollector Unify()
        {
            Words = Words.Distinct().ToList();
            Messages = Messages.Distinct().ToList();
            Columns = Columns.Distinct().ToList();
            Pages = Pages.Distinct().ToList();
            return this;
        }
    }
}
