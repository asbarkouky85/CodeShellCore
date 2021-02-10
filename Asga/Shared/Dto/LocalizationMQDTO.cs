using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Common.DTO
{
    public class LocalizationMQDTO
    {
        public long CurrentUser { get; set; }
        public long CurrentTenant { get; set; }
        public string Type { get; set; }
        public long Id { get; set; }
        public Dictionary<string, LocalizablesDTO> Locdto { get; set; }

    }
}
