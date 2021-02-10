using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocaleTextProvider
    {
        string Word(string index, string cult = null);
        string Word(string index, params string[] args);
        string WordWithCulture(string index, string cult, params string[] args);
        string Word(Enum en, string cult = null);
        string Column(string index, string cult = null);
        string Page(string index, string cult = null);
        string Message(string index, params string[] formatElements);
        string MessageWithCulture(string index, string cult, params string[] formatElements);
    }
}
