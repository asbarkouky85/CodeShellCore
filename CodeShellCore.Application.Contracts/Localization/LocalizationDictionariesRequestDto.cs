using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localization
{
    public class LocalizationDictionariesRequestDto
    {
        public string TenantCode { get; set; }
        public string Lang {  get; set; }   
    }
}
