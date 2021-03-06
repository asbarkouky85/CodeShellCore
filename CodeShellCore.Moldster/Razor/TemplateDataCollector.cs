﻿using CodeShellCore.Moldster.Dto;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Razor
{
    public class TemplateDataCollector
    {
        public string EntityName { get; set; }
        public List<ControlDTO> Controls { get; set; } = new List<ControlDTO>();
        public List<PageCategoryParameterDTO> Parameters { get; set; } = new List<PageCategoryParameterDTO>();
        public LocalizationDataCollector Localization { get; set; }
    }
}
